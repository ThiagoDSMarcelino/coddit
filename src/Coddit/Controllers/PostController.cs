using Coddit.Model;

namespace Coddit.Controllers;

[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostController : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<PostData>> Create(
        [FromBody] CreatePostData data,
        [FromServices] IRepository<Post> postRepo,
        [FromServices] IForumRepository forumRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var forum = await forumRepo.Get(forum => forum.Title == data.ForumTitle);

        if  (forum is null)
            return BadRequest();

        var newPost = new Post()
        {
            UserId = user.Id,
            ForumId = forum!.Id,
            Title = data.Title,
            Content = data.Content
        };

        await postRepo.Add(newPost);

        var result = new PostData()
        {
            Id = newPost.Id,
            Title = newPost.Title,
            Content = newPost.Content,
            CreateAt = DateTime.Now
        };

        return Created("", result);
    }

    [HttpPost]
    public async Task<ActionResult<List<PostData>>> GetPostByUser(
        [FromBody] UserData data,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] ISecurityService securityService,
        string q = "")
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var memberFrom = await memberRepo
            .FilterWithForumsAndPost(member =>
                member.UserId == user.Id &&
                member.Forum.Title.Contains(q)
            );

        var posts = memberFrom
            .Select(member => member.Forum)
            .SelectMany(forum => forum.Posts
                .Select(post => new PostData()
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreateAt = post.CreatedAt
                }))
            .OrderByDescending(post => post.CreateAt)
            .ToList();

        return posts;
    }

    [HttpPost("vote")]
    public async Task<ActionResult<PostData>> Vote(
        [FromBody] CreateVoteData data,
        [FromServices] IRepository<Post> postRepo,
        [FromServices] IRepository<Vote> voteRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var newVote = new Vote()
        {
            UserId = user.Id,
            PostId = data.Id,
            Value = data.Vote
        };

        await voteRepo.Add(newVote);

        var post = await postRepo.Get(post => post.Id == data.Id);

        var result = new PostData()
        {
            Id = post!.Id,
            Title = post.Title,
            Content = post.Content,
            CreateAt = post.CreatedAt
        };

        return result;
    }
}