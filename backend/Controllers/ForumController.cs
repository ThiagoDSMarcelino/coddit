namespace Coddit.Controllers;

[ApiController]
[Route("forum")]
[EnableCors("MainPolicy")]
public class ForumController : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<ForumData>> Create(
        [FromBody] CreateForum data,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] IRepository<Role> roleRepo,
        [FromServices] IForumRepository forumRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var usedTitle = await forumRepo.Exist(forum => forum.Title == data.Title);

        if (usedTitle)
            return BadRequest(new string[] { "This title for a forum is already take" });

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

        var result = new ForumData()
        {
            Title = forum.Title,
            Description = forum.Description,
            IsMember = true
        };


        return Created("", result);

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
        [FromServices] IRepository<Vote> voteRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var forum = await forumRepo.GetWithPost(f => f.Title == forumTitle);

        if (forum is null)
            return NotFound();

        var posts = new List<PostData>();
        foreach (var post in forum.Posts)
        {
            var vote = await voteRepo.Get(vote => vote.UserId == user.Id && vote.PostId == post.Id);

            var postData = new PostData()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreateAt = post.CreatedAt,
                Vote = vote?.Value
            };

            posts.Add(postData);
        }

        var result = new ForumPageData
        {
            Forum = new ForumData()
            {
                Title = forum.Title,
                Description = forum.Description,
                IsMember = forum.Members.Any(member => member.UserId == user.Id)
            },

            Posts = posts
                .OrderByDescending(post => post.CreateAt)
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
            return Unauthorized();

        var user = userValidate.User;

        var memberFrom = await memberRepo
            .FilterWithForums(member => member.UserId == user.Id);

        var forums = memberFrom
            .Select(member => new ForumData()
            {
                Title = member.Forum.Title,
                Description = member.Forum.Description,
                IsMember = true
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
            return Unauthorized();

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