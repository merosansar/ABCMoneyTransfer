namespace ABCMoneyTransfer.ViewModel
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public decimal TransferAmount { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderCountry { get; set; }
        public string SMobile { get; set; }
        public string SCurrency { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCountry { get; set; }
        public string RMobile { get; set; }
        public string RCurrency { get; set; }
        public decimal PayoutAmount { get; set; }
    }
}
