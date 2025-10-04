namespace Week2.Entities.Transactions
{
    internal class Transaction
    {
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public required Payment.Payment Payment { get; set; }
        public required Order.Order Order { get; set; }

        public override string ToString()
        {
            return $"{Payment}\n{Order}";
        }
    }
}
