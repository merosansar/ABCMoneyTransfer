namespace ABCMoneyTransfer.Models
{
    public class SendTransaction
    {


        //public int Id { get; set; }

        //public string? SenderName { get; set; }

        //public string? ReceiverName { get; set; }
        public string? SenderName { get; set; } = string.Empty;
        public string? ReceiverName { get; set; } = string.Empty;
        public string? SFirstName { get; set; }
            public string? SMidName { get; set; }

            public string? SLastName { get; set; }

            public string? RFirstName { get; set; }
            public string? RMidName { get; set; }

            public string? RLastName { get; set; }

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
        public string? SCountry { get; set; }
        public string? RCountry { get; set; }
        public string? SMobile { get; set; }

        public string? SIdentity { get; set; }
        public string? RMobile { get; set; }

        public string? RIdentity { get; set; }

        public decimal? Amount { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        





    }
}
