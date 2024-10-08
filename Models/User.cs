using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class User
{
    public Guid IdU { get; set; }

    public string NameU { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public virtual ICollection<GamesUser> GamesUsers { get; set; } = new List<GamesUser>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
