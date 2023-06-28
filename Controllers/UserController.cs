using Securitas.JWT;

namespace Coddit.Controllers;

using Coddit.DTO.User;
using Services;

[ApiController]
[Route("user")]
[EnableCors("MainPolicy")]
public class StudentController : ControllerBase
{
    [HttpPost("signup")]
    public async Task<ActionResult<UserData>> SignUp(
        [FromBody] UserData userData,
        [FromServices] IJWTService jwt,
        [FromServices] IRepository<User> repo,
        [FromServices] ISecurityService security)
    {
        var usedUsername = await repo.Exist(user => user.Username == userData.Username);
        var usedEmail = await repo.Exist(user => user.Email == userData.Email);
        var messages = new List<string>();
        
        if (usedEmail)
            messages.Add("E-mail already used");

        if (usedUsername)
            messages.Add("User-name already used");

        if (messages.Any())
            return BadRequest(messages);

        var salt = security.GenerateSalt(16);

        var newUser = new User()
        {
            Username = userData.Username,
            Email = userData.Email,
            Password = security.HashPassword(userData.Password, salt),
            Salt = salt,
            BirthDate = userData.BirthDate
        };

        await repo.Add(newUser);

        var result = new UserResult()
        {
            Token = jwt.GenerateToken(userData)
        };

        return Created("", result);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(
        [FromBody] UserData userData,
        [FromServices] IJWTService jwt,
        [FromServices] IRepository<User> repo,
        [FromServices] ISecurityService security)
    {
        Console.WriteLine(userData.Login);

        var user = await repo.Get(user =>
            user.Email == userData.Login ||
            user.Username == userData.Login);

        if (user is null)
            return BadRequest("Login isn't castrated");

        var hashedPassword = security.HashPassword(userData.Password, user.Salt);

        if (hashedPassword != user.Password)
            return BadRequest($"Inserted: {hashedPassword}\nCorrect: {user.Salt}\nSalt: {user.Salt}");

        var result = new UserResult()
        {
            Token = jwt.GenerateToken(userData)
        };

        return Ok(result);
    }
}