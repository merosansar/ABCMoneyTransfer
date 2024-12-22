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

    public string? Currency { get; set; }

    public string? SenderCountry { get; set; }

    public string? ReceiverCountry { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Status { get; set; }
}
