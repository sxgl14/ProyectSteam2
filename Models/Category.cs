using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class Category
{
    public Guid IdCat { get; set; }

    public string? NameCat { get; set; }

    public virtual ICollection<CategGame> CategGames { get; set; } = new List<CategGame>();
}
