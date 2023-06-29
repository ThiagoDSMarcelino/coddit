using Securitas.JWT;

namespace Coddit.Controllers;

using DTO;
using Model;
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
        [FromServices] IRepository<User> usersRepo,
        [FromServices] ISecurityService security)
    {
        var usedUsername = await usersRepo.Exist(user => user.Username == userData.Username);
        var usedEmail = await usersRepo.Exist(user => user.Email == userData.Email);
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

        await usersRepo.Add(newUser);

        var token = jwt.GenerateToken(userData);

        return Ok(new { token });
    }

    [HttpPost("signin")]
    public async Task<ActionResult<UserData>> SignIn(
        [FromBody] UserData userData,
        [FromServices] IJWTService jwt,
        [FromServices] IRepository<User> usersRepo,
        [FromServices] ISecurityService security)
    {
        var user = await usersRepo.Get(user =>
            user.Email == userData.Login ||
            user.Username == userData.Login);

        if (user is null)
            return BadRequest("Login is incorrect or isn't castrated");
        
        var hashedPassword = security.HashPassword(userData.Password, user.Salt);

        if (hashedPassword != user.Password)
            return BadRequest("Password is incorrect");


        var data = new JWTData()
        {
            UserId = user.Id,
            JWTCreateAt = DateTime.Now
        };  

        var token = jwt.GenerateToken(userData);

        return Ok(new { token });
    }
}