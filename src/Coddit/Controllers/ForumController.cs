namespace Coddit.Controllers;

using Model;
using DTO;
using Securitas.JWT;

[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]
public class ForumController : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] ForumData forumData,
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IRepository<User> userRepo,
        [FromServices] IJWTService jwt)
    {
        var validation = await jwt.ValidateTokenAsync<JWTData>(forumData.USerToken);

        if (!validation.IsValid)
            return BadRequest("Invalid Token");

        var usedTitle = await forumRepo.Exist(forum => forum.Title == forumData.Title);

        if (usedTitle)
            return BadRequest("This title for a forum is already take");

        var user = userRepo.Get(user => user.Id == validation.Data.UserId);

        var newForum = new Forum()
        {
            Title = forumData.Title,
            Description = forumData.Description,
        };

        await forumRepo.Add(newForum);

        var forum = forumRepo.Get(forum => forum.Title == newForum.Title);

        var admRole = new Role()
        {
            ForumId = forum.Id,
            Title = "ADM",
            IsOwner = true,
            IsDefault = false,
        };

        var defaultRole = new Role()
        {
            ForumId = forum.Id,
            Title = "Default",
            IsOwner = false,
            IsDefault = true,
        };

        // TODO


        return Ok();
    }

    //[HttpGet("{token}")]
    //public async Task<IActionResult> GetForuns(
    //    string token,
    //    [FromServices] IRepository<User> userRepo,
    //    [FromServices] IRepository<Forum> forumRepo,
    //    [FromServices] IJWTService jwt)
    //{
    //    var validationResult = await jwt.ValidateTokenAsync<JWTData>(token);

    //    if (!validationResult.IsValid)
    //        return BadRequest("Invalid token");

    //    var user = await userRepo.Get(user => user.Id == validationResult.Data.Id);

    //    if (user == null)
    //        return BadRequest();



    //    return Ok();
    //}
}