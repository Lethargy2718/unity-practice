namespace Week2.Entities.Payment
{
    internal class Cash : Payment
    {
        public double CashValue { get; set; }
        public override string ToString()
        {
            return base.ToString() + $" | Cash Value: {CashValue}";
        }
    }
}
