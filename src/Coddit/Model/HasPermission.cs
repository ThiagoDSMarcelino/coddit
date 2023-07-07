using System;
using System.Collections.Generic;

namespace Coddit.Model;

public partial class HasPermission
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public long PermissionId { get; set; }

    public bool Has { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
