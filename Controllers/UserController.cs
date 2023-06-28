using System.Security.Cryptography;
using Securitas.JWT;
using System.Text;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
[EnableCors("MainPolicy")]
public class StudentController : ControllerBase
{
    private readonly Encoding _encoding;


    public StudentController(Encoding encoding)
        => _encoding = encoding;

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(
        [FromBody] User user,
        [FromServices] IRepository<User, long> repo,
        [FromServices] IJWTService jwt)
    {
        var errors = new List<string>();
        var usedEmail = await repo.Exist(u => u.Email == user.Email);
        var usedUsername = await repo.Exist(u => u.Username == user.Username);
        
        if (usedEmail)
            errors.Add("E-mail already used");

        if (usedUsername)
            errors.Add("Username already used");

        if (errors.Any())
            return BadRequest(errors);

        var salt = GenerateSalt(16);

        user.Password = HashPassword(user.Password, salt);
        user.Salt = salt;

        await repo.Add(user);

        var token = jwt.GenerateToken(user);

        return Created("http://localhost:4200/", token);
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

    private string GenerateSalt(int saltLength)
    {
        var length = (int)MathF.Floor(saltLength * (6 / 8));
        byte[] randBytes = new byte[length];
        Random.Shared.NextBytes(randBytes);
        var salt = Convert.ToBase64String(randBytes);

        return salt;
    }
}