using System;
using System.Collections.Generic;

namespace Coddit.Model;

public partial class Permission
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<HasPermission> HasPermissions { get; set; } = new List<HasPermission>();
}
