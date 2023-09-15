namespace Coddit.Controllers;

[ApiController]
[Route("vote")]
[EnableCors("MainPolicy")]
public class VoteController : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(
        [FromBody] VoteData data,
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
            PostId = data.PostId,
            Value = data.Vote
        };

        await voteRepo.Add(newVote);

        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(
        [FromBody] VoteData data,
        [FromServices] IRepository<Vote> voteRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var vote = await voteRepo.Get(vote => vote.UserId == user.Id && vote.PostId == data.PostId);

        if (vote is null)
            return BadRequest();

        vote.Value = data.Vote;

        return Ok();
    }

    [HttpPost("delete")]
    public async Task<IActionResult>Delete(
        [FromBody] VoteData data,
        [FromServices] IRepository<Vote> voteRepo,
        [FromServices] ISecurityService securityService)
    {
        var userValidate = await securityService.ValidateUserAsync(data.Token);

        if (userValidate.User is null)
            return Unauthorized();

        var user = userValidate.User;

        var vote = await voteRepo.Get(vote => vote.UserId == user.Id && vote.PostId == data.PostId);

        if (vote is null)
            return BadRequest();

        await voteRepo.Delete(vote);

        return Ok();
    }
}