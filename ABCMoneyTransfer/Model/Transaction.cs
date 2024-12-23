using System;
using System.Collections.Generic;

namespace ABCMoneyTransfer.Model;

public partial class Transaction
{
    public int Id { get; set; }

    public string? SenderName { get; set; }

    public string? ReceiverName { get; set; }

    public string? SenderAddress { get; set; }

    public string? ReceiverAddress { get; set; }

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public decimal? TransferAmount { get; set; }

    public decimal? PayoutAmount { get; set; }

    public string? Scurrency { get; set; }

    public string? SenderCountry { get; set; }

    public string? ReceiverCountry { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Status { get; set; }

    public string? Sidentity { get; set; }

    public string? Ridentity { get; set; }

    public string? Smobile { get; set; }

    public string? Rmobile { get; set; }

    public string? Rcurrency { get; set; }

    public decimal? ExchangeRate { get; set; }

    public decimal? ServiceCharge { get; set; }

    public DateTime SendDate { get; set; }
}
