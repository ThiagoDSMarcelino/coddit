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
        [FromServices] IRepository<Vote> voteRepo,
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

        var vote = await voteRepo.Get(vote => vote.UserId == user.Id && vote.PostId == newPost.Id);

        var result = new PostData()
        {
            Id = newPost.Id,
            Title = newPost.Title,
            Content = newPost.Content,
            CreateAt = DateTime.Now,
            Vote = vote?.Value
        };

        return Created("", result);
    }

    [HttpPost]
    public async Task<ActionResult<List<PostData>>> GetPostByUser(
        [FromBody] UserData data,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] IRepository<Vote> voteRepo,
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
            .SelectMany(forum => forum.Posts);

        var result = new List<PostData>();

        foreach (var post in posts)
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

            result.Add(postData);
        }

        result = result
            .OrderByDescending(post => post.CreateAt)
            .ToList();

        return result;
    }
}