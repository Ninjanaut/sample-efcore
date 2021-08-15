using System;
using System.Collections.Generic;

namespace App.Domain
{
    public class Order
    {
        public int? Id { get; private set; }
        public int CustomerId { get; private set; }
        public int Status { get; private set; }
        public DateTime OrderDate { get; private set; }

        // Navigation property
        public List<OrderLine> Lines { get; private set; } =
            new List<OrderLine>();

        // EF constructor
        private Order() { }

        public Order(Customer customer)
        {
            CustomerId = (int)customer.Id;
            Status = (int)OrderStatus.Ordered;
        }

        public void AddItem(OrderLine item)
        {
            Lines.Add(item);
        }
    }
}
