#pragma warning disable CS8618

namespace Coddit.DTO;

public class PostData
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreateAt { get; set; }
    public string ForumName { get; set; }
}