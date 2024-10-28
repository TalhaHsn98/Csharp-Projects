using System;

class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        int choice;

        do
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. View Inventory");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Product ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Enter Description: ");
                    string description = Console.ReadLine();

                    Product product = new Product(id, name, price, quantity, description);
                    inventory.AddProduct(product);
                    break;

                case 2:
                    Console.Write("Enter Product ID to Update: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Quantity: ");
                    int newQuantity = int.Parse(Console.ReadLine());
                    Console.Write("Enter New Price: ");
                    decimal newPrice = decimal.Parse(Console.ReadLine());

                    inventory.UpdateProduct(updateId, newQuantity, newPrice);
                    break;

                case 3:
                    Console.Write("Enter Product ID to Delete: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    inventory.DeleteProduct(deleteId);
                    break;

                case 4:
                    inventory.ViewInventory();
                    break;

                case 5:
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (choice != 5);
    }
}
