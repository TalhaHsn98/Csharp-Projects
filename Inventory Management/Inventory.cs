using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management
{
   
        public class Inventory
        {
            private List<Product> products = new List<Product>();

            public void AddProduct(Product product)
            {
                products.Add(product);
                Console.WriteLine("Product added successfully.");
            }

            public void ViewProducts()
            {
                Console.WriteLine("\nCurrent Inventory:");
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }

            public void UpdateProduct(int id, int quantity, decimal price)
            {
                var product = products.Find(p => p.ID == id);
                if (product != null)
                {
                    product.Quantity = quantity;
                    product.Price = price;
                    Console.WriteLine("Product updated successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

            public void DeleteProduct(int id)
            {
                var product = products.Find(p => p.ID == id);
                if (product != null)
                {
                    products.Remove(product);
                    Console.WriteLine("Product deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }
    

}
