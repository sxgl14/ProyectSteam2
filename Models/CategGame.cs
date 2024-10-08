using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class CategGame
{
    public Guid IdCg { get; set; }

    public Guid? IdGame { get; set; }

    public Guid? IdCateg { get; set; }

    public virtual Category? IdCategNavigation { get; set; }

    public virtual Game? IdGameNavigation { get; set; }
}
