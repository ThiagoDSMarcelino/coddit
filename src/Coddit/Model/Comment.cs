namespace Coddit.Model;

public partial class Comment
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long? PostId { get; set; }

    public long? CommentId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Comment? CommentNavigation { get; set; }

    public virtual ICollection<Comment> InverseCommentNavigation { get; set; } = new List<Comment>();

    public virtual Post? Post { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
