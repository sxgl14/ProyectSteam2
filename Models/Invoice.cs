using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class Invoice
{
    public Guid IdI { get; set; }

    public string? Dateti { get; set; }

    public Guid? IdG { get; set; }

    public Guid? IdU { get; set; }

    public virtual Game? IdGNavigation { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
