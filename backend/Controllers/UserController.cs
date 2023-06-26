using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
[EnableCors("MainPolicy")]
public class StudentController : ControllerBase
{
    private readonly Encoding _encoding;

    StudentController(Encoding encoding)
        => _encoding = encoding;

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(
        [FromBody] User user,
        [FromServices] IRepository<User> repo)
    {
        var salt = GenerateSalt();

        user.Password = HashPassword(user.Password, salt);
        user.Salt = salt;

        await repo.Add(user);

        return Ok();
    }

    private string HashPassword(string password, string salt)
    {
        var encryptedBytes = ConcatPasswordSalt(password, salt);

        using var sha = SHA256.Create();
        var hashedPasswordBytes = sha.ComputeHash(encryptedBytes);
        var hashedPassword = _encoding.GetString(hashedPasswordBytes);

        return hashedPassword; 
    }

    private byte[] ConcatPasswordSalt(string password, string salt)
    {
        var passwordBytes = _encoding.GetBytes(password);
        var saltBytes = _encoding.GetBytes(salt);
        
        var concatBytes = new byte[passwordBytes.Length + saltBytes.Length];
        passwordBytes.CopyTo(concatBytes, 0);
        saltBytes.CopyTo(concatBytes, passwordBytes.Length);

        return concatBytes;
    }

    private string GenerateSalt()
    {
        byte[] randBytes = new byte[12];
        Random.Shared.NextBytes(randBytes);
        var salt = _encoding.GetString(randBytes);

        return salt;
    }
}