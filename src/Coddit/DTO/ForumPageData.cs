#pragma warning disable CS8618

namespace Coddit.DTO;

public class ForumPageData
{
    public ForumData Forum { get; set; }
    public List<PostData> Posts { get; set; }
}