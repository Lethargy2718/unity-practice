namespace Week2.Entities.Payment
{
    internal abstract class Payment
    {
        public DateTime PaidDate { get; set; } = DateTime.Now;
        private double Amount { get; set; }

        // What's this supposed to do..?
        public double Pay()
        {
            return Amount;
        }

        // This too??
        public string Update()
        {
            return "";
        }
    }
}
