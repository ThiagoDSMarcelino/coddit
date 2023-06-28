namespace Backend.Model;

public partial class Member
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long ForumId { get; set; }

    public long RoleId { get; set; }

    public virtual Forum Forum { get; set; }

    public virtual Role Role { get; set; }

    public virtual User User { get; set; }
}
