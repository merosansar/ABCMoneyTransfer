using System;
using System.Collections.Generic;

namespace ABCMoneyTransfer.Model;

public partial class Sender
{
    public int SenderId { get; set; }

    public string? SfirstName { get; set; }

    public string? SmidName { get; set; }

    public string? SlastName { get; set; }

    public string? SenderAddress { get; set; }

    public string? Scountry { get; set; }

    public string? Smobile { get; set; }

    public string? Sidentity { get; set; }
}
