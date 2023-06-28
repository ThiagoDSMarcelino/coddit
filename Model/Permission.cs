namespace Coddit.Model;

public partial class Permission
{
    public long Id { get; set; }

    public string Title { get; set; }

    public virtual ICollection<HasPermission> HasPermissions { get; set; } = new List<HasPermission>();
}
