using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Forum
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
