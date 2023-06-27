namespace Backend.Model;

public partial class Post
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ForumId { get; set; }

    public string Title { get; set; }

    public string Message { get; set; }

    public byte[] Attachment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Forum Forum { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
