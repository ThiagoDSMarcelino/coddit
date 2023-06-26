using System;
using System.Collections.Generic;

namespace Backend.Model;

public partial class Vote
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? PostId { get; set; }

    public long? CommentId { get; set; }

    public bool Value { get; set; }

    public virtual Comment Comment { get; set; }

    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
