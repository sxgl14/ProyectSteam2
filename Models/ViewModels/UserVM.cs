using System;
using System.Collections.Generic;

namespace ProyectSteam2.Models;

public partial class UserVM
{
    public Guid IdU { get; set; }
    public string NameU { get; set; } = null!;
    public string Pass { get; set; } = null!;
    public string ConfirmPass { get; set; } = null!;
    public string Mail { get; set; } = null!;
}
