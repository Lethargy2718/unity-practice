namespace Week2.Entities.Payment
{
    internal class Check : Payment
    {
        public required string Name { get; set; }
        public required string BankId { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" | Name: {Name} | Bank ID: {BankId}";
        }
    }
}
