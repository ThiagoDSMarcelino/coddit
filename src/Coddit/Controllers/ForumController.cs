using Securitas.JWT;

namespace Coddit.Controllers;

using Model;
using DTO;

[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]
public class ForumController : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] ForumData forumData,
        [FromServices] IRepository<Member> memberRepo,
        [FromServices] IRepository<Role> roleRepo,
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IRepository<User> userRepo,
        [FromServices] IJWTService jwt)
    {
        var validation = await jwt.ValidateTokenAsync<JWTData>(forumData.UserToken);

        if (!validation.IsValid)
            return BadRequest("Invalid Token");

        var usedTitle = await forumRepo.Exist(forum => forum.Title == forumData.Title);

        if (usedTitle)
            return BadRequest("This title for a forum is already take");


        var newForum = new Forum()
        {
            Title = forumData.Title,
            Description = forumData.Description,
        };

        await forumRepo.Add(newForum);

        var forum = forumRepo.Get(forum => forum.Title == newForum.Title);

        await CreateDefaultRoles(forum.Id, roleRepo);

        var user = userRepo.Get(user => user.Id == validation.Data.UserId);
        var adm = roleRepo.Get(role => role.ForumId == forum.Id && role.IsOwner);

        var firstMember = new Member()
        {
            UserId = user.Id,
            ForumId = forum.Id,
            RoleId = adm.Id
        };

        await memberRepo.Add(firstMember);

        return Ok();
    }

    //private static async Task<IActionResult> CreateDefaultRoles(int forumId, IRepository<Role> roleRepo)
    //{
    //    var admRole = new Role()
    //    {
    //        ForumId = forumId,
    //        Title = "ADM",
    //        IsOwner = true,
    //        IsDefault = false,
    //    };

    //    var defaultRole = new Role()
    //    {
    //        ForumId = forumId,
    //        Title = "Default",
    //        IsOwner = false,
    //        IsDefault = true,
    //    };

    //    await roleRepo.Add(admRole);
    //    await roleRepo.Add(defaultRole);
    //}

    //[HttpGet("{token}")]
    //public async Task<IActionResult> GetForuns(
    //    string token,
    //    [FromServices] IRepository<User> userRepo,
    //    [FromServices] IRepository<Forum> forumRepo,
    //    [FromServices] IRepository<Member> memberRepo,
    //    [FromServices] IJWTService jwt)
    //{
    //    var validationResult = await jwt.ValidateTokenAsync<JWTData>(token);

    //    if (!validationResult.IsValid)
    //        return BadRequest("Invalid token");

    //    var user = await userRepo.Get(user => user.Id == validationResult.Data.UserId);

    //    if (user == null)
    //        return BadRequest();

    //    var memberFrom = await memberRepo.Filter(member => member.UserId == user.Id);

    //    var forumIds = memberFrom.Select(member => member.ForumId);

    //    var forums = await forumRepo.Filter(forum => forumIds.Contains(forum.Id));

    //    return Ok(forums);
    //}
}