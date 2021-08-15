using System.Collections.Generic;

namespace App.Domain
{
    public class Customer
    {
        public int? Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        // Navigation properties
        public Address Address { get; private set; }
        public List<Order> Orders { get; private set; }

        // EF constructor
        private Customer() { }

        public Customer(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
    }
}
