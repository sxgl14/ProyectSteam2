using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class LoginVM
{
    public string NameU { get; set; } = null!;
    public string Mail { get; set; } = null!;
    public string Pass { get; set; } = null!;
}
