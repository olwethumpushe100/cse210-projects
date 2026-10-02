using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Maple Street", "Rexburg", "ID", "USA");
        Customer customer1 = new Customer("Emily Carter", address1);

        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Mouse", "P1001", 25.00, 2));
        order1.AddProduct(new Product("USB-C Cable", "P1002", 8.50, 3));

        Address address2 = new Address("45 Long Street", "Cape Town", "Western Cape", "South Africa");
        Customer customer2 = new Customer("Olwethu Mpushe", address2);

        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Notebook", "N2001", 3.00, 5));
        order2.AddProduct(new Product("Pen Set", "P3003", 12.00, 1));
        order2.AddProduct(new Product("Backpack", "B4004", 45.00, 1));

        DisplayOrder(order1);
        DisplayOrder(order2);
    }

    static void DisplayOrder(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());

        double totalCost = order.GetTotalCost();
        Console.WriteLine($"Total Price: ${totalCost.ToString("F2", CultureInfo.InvariantCulture)}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine();
    }
}
