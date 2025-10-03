namespace Week2.Entities.Customers
{
    internal class Company : Customer
    {
        public required string Location { get; set; }
        public required string CompanyName { get; set; }

        public void Edit(string? phone = null, string? location = null, string? companyName = null)
        {
            base.Edit(phone);
            if (location is not null) Location = location;
            if (companyName is not null) CompanyName = companyName;
        }

        public override string ToString() => base.ToString() + $" | Location: {Location} | Company: {CompanyName}";
    }
}
