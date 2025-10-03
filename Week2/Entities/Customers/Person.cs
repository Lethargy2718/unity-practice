namespace Week2.Entities.Customers
{
    internal class Person : Customer
    {
        public required string BillingAddress { get; set; }
        public required string FullName { get; set; }

        public void Edit(string? phone = null, string? billingAddress = null, string? fullAddress = null)
        {
            base.Edit(phone);
            if (billingAddress is not null) BillingAddress = billingAddress;
            if (fullAddress is not null) FullName = fullAddress;
        }

        public override string ToString() => base.ToString() + $" | Billing Address: {BillingAddress} | Full Name: {FullName}";
    }
}
