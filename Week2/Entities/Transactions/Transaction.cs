using Week2.Entities.Payment;
using Week2.Entities.Order;

namespace Week2.Entities.Transactions
{
    internal class Transaction
    {
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public Payment.Payment Payment { get; set; }
        public Order.Order Order { get; set; }

    }
}
