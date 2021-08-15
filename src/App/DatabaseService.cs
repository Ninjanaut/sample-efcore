using App.Database;
using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace App
{
    public class DatabaseService
    {
        private readonly AppDbContext _context;
        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }
        public void PopulateDatabase()
        {
            // Create customers and products
            var padme = 
                new Customer("Padmé", "Amidala", 
                new Address("Palace of Theed", "Palace Plaza", "1"));

            var jarjar = 
                new Customer("Jar Jar", "Binks", 
                new Address("Gungan City", "Otoh Villages", "12"));

            var blaster = 
                new Product(78954, "ELG-3A - lightweight, elegant, and functional hold-out blaster pistol");

            var shield = 
                new Product(98746, "Gungan personal energy shield");

            // Save customers and products to database
            _context.Customers.Add(padme);
            _context.Customers.Add(jarjar);
            _context.Products.Add(blaster);
            _context.Products.Add(shield);
            _context.SaveChanges();

            // Create orders
            var padmeOrder = new Order(padme);
            padmeOrder.AddItem(new OrderLine(blaster, 1, 9999));
            padmeOrder.AddItem(new OrderLine(shield, 1, 5000));

            var jarjarOrder = new Order(jarjar);
            jarjarOrder.AddItem(new OrderLine(shield, 2, 4500));

            // Save orders to database
            _context.Orders.Add(padmeOrder);
            _context.Orders.Add(jarjarOrder);
            _context.SaveChanges();
        }

        public void ReadDatabase()
        {
            var customers = 
                _context.Customers
                    .Include(x => x.Address)
                    .Include(x => x.Orders)
                    .ThenInclude(x => x.Lines)
                    .ThenInclude(x => x.Product);

            foreach (var customer in customers)
            {
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");

                Console.WriteLine($"City: {customer.Address.City}");

                foreach (var order in customer.Orders)
                {
                    foreach (var line in order.Lines)
                    {
                        Console.WriteLine(
                            $"Item: {line.Product.Name}, " +
                            $"Quantity: {line.Quantity}, " +
                            $"Total Price: {line.UnitPrice * line.Quantity}");
                    }
                }

                Console.WriteLine();
            }
        }

        public void EmptyDatabase()
        {
            // Before reseeding the tables, we check them for values to prevent record with id 0. 
            var query = @"
                truncate table dbo.[Address];
                truncate table dbo.[OrderLine];

                if exists(select * from dbo.Product)
                begin
                    delete dbo.Product;
                    dbcc checkident('dbo.Product', reseed, 0);
                end

                if exists(select * from dbo.[Order])
                begin
                    delete dbo.[Order];
                    dbcc checkident('dbo.Order', reseed, 0);
                end

                if exists(select * from dbo.Customer)
                begin
                    delete dbo.Customer;
                    dbcc checkident('dbo.Customer', reseed, 0);
                end";
            _context.Database.ExecuteSqlRaw(query);
        }
    }
}
