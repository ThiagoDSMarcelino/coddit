namespace Coddit.Controllers;

using DTO;
using Repositories;
using Services;

[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostController : ControllerBase
{
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
