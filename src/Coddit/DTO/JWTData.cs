#pragma warning disable CS8618

namespace Coddit.DTO;

public class JWTData
{
    public long UserId { get; set; }
    public DateTime JWTCreateAt { get; set; }
}