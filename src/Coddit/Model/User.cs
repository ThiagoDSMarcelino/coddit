namespace Coddit.Model;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public byte[]? Picture { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
