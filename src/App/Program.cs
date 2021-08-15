using App.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

            var optionsBuilder =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connectionString);

            using var context = new AppDbContext(optionsBuilder.Options);

            var databaseService = new DatabaseService(context);

            databaseService.EmptyDatabase();
            databaseService.PopulateDatabase();
            databaseService.ReadDatabase();

            Console.WriteLine("Program ends.");
            Console.ReadLine();
        }
    }
}
