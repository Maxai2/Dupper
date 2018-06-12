using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dupper
{
    class Program
    {
        static void Main(string[] args)
        { 
            ProductsDBEntities db = new ProductsDBEntities();

            db.Database.Log = (log) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(log);
                Console.ForegroundColor = ConsoleColor.White;
            };

            foreach (var item in db.Customers)
            {
                Console.WriteLine(item.FirstName);
            }

            //var customers = from c in db.Customers
            //                where c.Orders.Count() == 0
            //                select c;

            //var customers = db.Customers.Where(c => c.Orders.Count() == 0);

            //var custumers = db.Database.SqlQuery<Customer>("SELECT * FROM Customers WHERE Id != ALL (SELECT CustomerId FROM Orders)");

            //var customers = db.Customers.Join(db.Orders, c => c.Id, o => o.CustomerId, (c, o) => new { CustomerName = c.FirstName, Date = o.CreatedAt, Price = o.Price });

            //foreach (var item in customers)
            //{
            //    Console.WriteLine($"{item.CustomerName}\t{item.Date}\t{item.Price}");
            //}

            //var customers = db.Customers.Join(db.Orders, c => c.Id, o => o.CustomerId, (c, o) => new { CustomerName = c.FirstName, Date = o.CreatedAt, Price = o.Price, ProductId = o.ProductId }).Join(db.Products, a => a.ProductId, p => p.Id, (a, p) => new { CustomerName = a.CustomerName, Date = a.Date, Price = a.Price, ProductName = p.ProductName});

            //foreach (var item in customers)
            //{
            //    Console.WriteLine($"{item.CustomerName}\t{item.Date}\t{item.Price}\t{item.ProductName}");
            //}

            var groups = db.Customers.Join(db.Orders, c => c.Id, o => o.CustomerId, (c, o) => new { CustomerName = c.FirstName, Date = o.CreatedAt, Price = o.Price, ProductId = o.ProductId }).Join(db.Products, a => a.ProductId, p => p.Id, (a, p) => new { CustomerName = a.CustomerName, Date = a.Date, Price = a.Price, ProductName = p.ProductName }).GroupBy(c => c.CustomerName);

            foreach (var item in groups)
            {
                Console.WriteLine(item.Key + ":");

                foreach (var c in item)
                {
                    Console.WriteLine($"{c.Date}\t{c.Price}\t{c.ProductName}");
                }
            }

        }
    }
}
