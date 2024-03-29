using Securitas.JWT;

namespace Coddit.Controllers;

[ApiController]
[Route("user")]
[EnableCors("MainPolicy")]
public class UserController : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<UserData>> Create(
        [FromBody] CreateUser userData,
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

        var user = usersRepo.Get(user => 
            user.Username == userData.Username && 
            user.Email == userData.Email);

        var data = new JWTData()
        {
            UserId = user.Id,
            CreateAt = DateTime.Now.ToString()
        };

        var token = jwt.GenerateToken(data);

        var tokenData = new UserData()
        {
            Token = token
        };

        return Created("", tokenData);
    }

    [HttpPost]
    public async Task<ActionResult<UserData>> Get(
        [FromBody] LoginUser userData,
        [FromServices] IJWTService jwt,
        [FromServices] IRepository<User> usersRepo,
        [FromServices] ISecurityService security)
    {
        var user = await usersRepo.Get(u =>
            u.Email == userData.Login ||
            u.Username == userData.Login);

        if (user is null)
            return BadRequest(new string[] { "Login is incorrect" });
        
        var hashedPassword = security.HashPassword(userData.Password, user.Salt);

        if (hashedPassword != user.Password)
            return BadRequest(new string[] { "Password is incorrect" });


        var data = new JWTData()
        {
            UserId = user.Id,
            CreateAt = DateTime.Now.ToString()
        };  

        var token = jwt.GenerateToken(data);

        var tokenData = new UserData()
        {
            Token = token
        };

        return tokenData;
    }
}