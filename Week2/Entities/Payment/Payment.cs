namespace Week2.Entities.Payment
{
    internal abstract class Payment
    {
        public DateTime PaidDate { get; set; } = DateTime.Now;
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"Paid Date: {PaidDate} | Amount: {Amount}";
        }
    }
}
