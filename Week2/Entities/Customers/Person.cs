namespace Week2.Entities.Customers
{
    internal class Person : Customer
    {
        public required string BillingAddress { get; set; }
        public required string FullName { get; set; }

        public void Edit(string? phone = null, string? billingAddress = null, string? fullName = null)
        {
            base.Edit(phone);
            if (billingAddress is not null) BillingAddress = billingAddress;
            if (fullName is not null) FullName = fullName;
        }

        public override string ToString() => base.ToString() + $" | Billing Address: {BillingAddress} | Full Name: {FullName}";
    }
}
