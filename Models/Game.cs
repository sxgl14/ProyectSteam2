using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class Game
{
    public Guid IdG { get; set; }

    public string NameG { get; set; } = null!;

    public string DateR { get; set; } = null!;

    public decimal? Price { get; set; }

    public string Creator { get; set; } = null!;

    public string? ImgGame { get; set; }

    public string? Descript { get; set; }

    public virtual ICollection<CategGame> CategGames { get; set; } = new List<CategGame>();

    public virtual ICollection<GamesUser> GamesUsers { get; set; } = new List<GamesUser>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
