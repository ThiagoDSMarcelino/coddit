namespace Coddit.Model;

public partial class Role
{
    public long Id { get; set; }

    public long ForumId { get; set; }

    public string Title { get; set; } = null!;

    public bool IsOwner { get; set; }

    public bool IsDefault { get; set; }

    public virtual Forum Forum { get; set; } = null!;

    public virtual ICollection<HasPermission> HasPermissions { get; set; } = new List<HasPermission>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
