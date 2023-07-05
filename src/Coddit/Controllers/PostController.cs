using Securitas.JWT;

namespace Coddit.Controllers;

using DTO;
using Repositories.MemberReposiory;

[ApiController]
[Route("post")]
[EnableCors("MainPolicy")]
public class PostController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<List<PostData>>> GetPostByUser(
        [FromBody] UserData data,
        [FromServices] IRepository<User> userRepo,
        [FromServices] IRepository<Forum> forumRepo,
        [FromServices] IMemberRepository memberRepo,
        [FromServices] IJWTService jwt,
        string q = "")
    {
        var validation = await jwt.ValidateTokenAsync<JWTData>(data.Token);

        if (!validation.IsValid)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "Invalid Token"
            };

            return BadRequest(error);
        }

        var user = await userRepo.Get(user => user.Id == validation.Data.UserId);

        if (user is null)
        {
            var error = new ErrorData
            {
                Messages = Array.Empty<string>(),
                Reason = "User don't found"
            };

            return BadRequest(error);
        }


        var memberFrom = await memberRepo
            .FilterWithForums(member => member.UserId == user.Id);

        var forums = await forumRepo.Filter(f => true);

        var posts = memberFrom
            .Where(member => member.Forum.Title.Contains(q))
            .SelectMany(member => member.Forum.Posts)
            .Select(post => new PostData()
                {
                    Title = post.Title,
                    Content = post.Content,
                    CreateAt = post.CreatedAt,
                    ForumName = forums.First(f => f.Id == post.ForumId).Title
                })
            .ToList();

        Console.WriteLine(memberFrom.Count);
        Console.WriteLine(forums.Count);
        Console.WriteLine(posts.Count);

        return posts;
    }
}
