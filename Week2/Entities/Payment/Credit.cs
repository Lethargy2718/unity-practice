namespace Week2.Entities.Payment
{
    internal class Credit : Payment
    {
        public required string Number { get; set; }
        public DateTime ExpireDate { get; set; }
        public required string Type { get; set; }
    }
}
