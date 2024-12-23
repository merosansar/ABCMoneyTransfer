using System;
using System.Collections.Generic;

namespace ABCMoneyTransfer.Model;

public partial class Receiver
{
    public int ReceiverId { get; set; }

    public string? RfirstName { get; set; }

    public string? RmidName { get; set; }

    public string? RlastName { get; set; }

    public string? ReceiverAddress { get; set; }

    public string? Rcountry { get; set; }

    public string? Rmobile { get; set; }

    public string? Ridentity { get; set; }
}
