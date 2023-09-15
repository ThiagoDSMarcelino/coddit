namespace Coddit.DTO;

public class VoteData
{
    public string Token { get; set; }
    public bool Vote { get; set; }
    public long PostId { get; set; }
}