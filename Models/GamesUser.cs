using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class GamesUser
{
    public Guid IdGU { get; set; }

    public Guid? IdU { get; set; }

    public Guid? IdG { get; set; }

    public virtual Game? IdGNavigation { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
