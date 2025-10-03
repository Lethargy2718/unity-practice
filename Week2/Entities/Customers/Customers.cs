using Week2.Entities.Repository;

namespace Week2.Entities.Customers
{
    internal class Customers : Repository<Customer>
    {
        protected override int GetId(Customer customer) => customer.Id;

        public Customer EditCustomer(int customerId, string? phone = null, string? billingAddress = null, string? fullName = null, string? location = null, string? companyName = null)
        {
            var customer = Find(customerId);

            if (customer is Person person)
            {
                person.Edit(phone, billingAddress, fullName);
            }
            else if (customer is Company company)
            {
                company.Edit(phone, location, companyName);
            }

            return customer;
        }
    }
}