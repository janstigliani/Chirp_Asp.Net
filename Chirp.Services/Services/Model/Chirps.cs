using System;
using System.Collections.Generic;

namespace Chirp.Model;

public partial class Chirps
{
    public int ChirpsId { get; set; }

    public DateTime CreationTime { get; set; }

    public string Text { get; set; } = null!;

    public string? ExternalUrl { get; set; }

    public double? Lat { get; set; }

    public double? Lng { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
