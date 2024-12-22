using System;
using System.Collections.Generic;

namespace ABCMoneyTransfer.Model;

public partial class ExchangeRate
{
    public int Id { get; set; }

    public string BaseCurrency { get; set; } = null!;

    public string TargetCurrency { get; set; } = null!;

    public decimal ExchangeRate1 { get; set; }

    public DateTime LastUpdated { get; set; }
}
