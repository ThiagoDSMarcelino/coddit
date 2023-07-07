namespace Coddit.Controllers;

using DTO;
using Model;
using Repositories;
using Services;

[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]
public class ForumController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] CreateForum data,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] IRepository<Role> roleRepo,
        [FromServices] IForumRepository forumRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "Invalid Token",
            };

            return BadRequest(error);
        }

        var user = userValidate.User;

        var usedTitle = await forumRepo.Exist(forum => forum.Title == data.Title);

        if (usedTitle)
        {
            var error = new ErrorData
            {
                Messages = new string[] { "This title for a forum is already take" },
                Reason = "A forum with this title already exist"
            };

            return BadRequest(error);
        }

        var newForum = new Forum()
        {
            Title = data.Title,
            Description = data.Description,
        };

        await forumRepo.Add(newForum);

        var forum = await forumRepo.Get(forum => forum.Title == newForum.Title);
        
        await CreateDefaultRoles(forum!.Id, roleRepo);

        var adm = await roleRepo.Get(role => role.ForumId == forum.Id && role.IsOwner);

        var firstMember = new Member()
        {
            UserId = user.Id,
            ForumId = forum.Id,
            RoleId = adm!.Id
        };

        await memberRepo.Add(firstMember);

        return Created("", new { });

        static async Task CreateDefaultRoles(long forumId, IRepository<Role> roleRepo)
        {
            var admRole = new Role()
            {
                ForumId = forumId,
                Title = "ADM",
                IsOwner = true,
                IsDefault = false,
            };

            var defaultRole = new Role()
            {
                ForumId = forumId,
                Title = "Default",
                IsOwner = false,
                IsDefault = true,
            };

            await roleRepo.Add(admRole);
            await roleRepo.Add(defaultRole);
        }
    }
    
    [HttpPost("{forumTitle}")]
    public async Task<ActionResult<ForumPageData>> Get(
        string forumTitle,
        [FromBody] UserData data,
        [FromServices] IForumRepository forumRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "Invalid Token",
            };

            return BadRequest(error);
        }

        var user = userValidate.User;

        var forum = await forumRepo.GetWithPost(f => f.Title == forumTitle);

        if (forum is null)
            return NotFound();

        var result = new ForumPageData
        {
            Forum = new ForumData()
            {
                Title = forum.Title,
                Description = forum.Description,
                IsMember = forum.Members.Any(member => member.UserId == user.Id)
            },

            Posts = forum.Posts
                .Select(post => new PostData()
                {
                    Title = post.Title,
                    Content = post.Content,
                    CreateAt = post.CreatedAt,
                    ForumName = forum.Title
                })
                .ToList()
        };

        return result;
    }

    [HttpPost("byUser")]
    public async Task<ActionResult<List<ForumData>>> GetByUser(
        [FromBody] UserData data,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "Invalid Token",
            };

            return BadRequest(error);
        }

        var user = userValidate.User;

        var memberFrom = await memberRepo
            .FilterWithForums(member => member.UserId == user.Id);

        var forums = memberFrom
            .Select(member => new ForumData()
            {
                Title = member.Forum.Title,
                Description = member.Forum.Description,
                IsMember = member.UserId == user.Id
            })
            .ToList();

        return forums;
    }

    [HttpPost]
    public async Task<ActionResult<List<ForumData>>> GetAll(
        [FromBody] UserData data,
        [FromServices] IForumRepository forumRepo,
        [FromServices] ISecurityService securityService,
        string q = "")
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "Invalid Token",
            };

            return BadRequest(error);
        }

        var user = userValidate.User;

        var allForums = await forumRepo.FilterWithMembers(f => f.Title.Contains(q));

        var forums = allForums
            .Select(forum => new ForumData()
            {
                Title = forum.Title,
                Description = forum.Description,
                IsMember = forum.Members.Any(member => member.UserId == user.Id)
            })
            .ToList();

        return forums;
    }
}