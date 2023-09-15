namespace Coddit.Model;

public partial class Member
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ForumId { get; set; }

    public long RoleId { get; set; }

    public virtual Forum Forum { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
