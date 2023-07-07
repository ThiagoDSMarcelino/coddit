namespace Coddit.Controllers;

[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] CreatePostData data,
        [FromServices] IRepository<Post> postRepo,
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

        var forum = await forumRepo.Get(forum => forum.Title == data.ForumTitle);

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
            Title = newPost.Title,
            Content = newPost.Content,
            CreateAt = DateTime.Now,
            ForumName = forum.Title
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
            .FilterWithForumsAndPost(member =>
                member.UserId == user.Id &&
                member.Forum.Title.Contains(q)
            );

        var posts = memberFrom
            .Select(member => member.Forum)
            .SelectMany(forum => forum.Posts
                .Select(post => new PostData()
                {
                    Title = post.Title,
                    Content = post.Content,
                    CreateAt = post.CreatedAt,
                    ForumName = forum.Title
                })
            ).ToList();

        return posts;
    }
}
