using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management
{

        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public Product(int id, string name, int quantity, decimal price)
            {
                ID = id;
                Name = name;
                Quantity = quantity;
                Price = price;
            }

            public override string ToString()
            {
                return $"ID: {ID}, Name: {Name}, Quantity: {Quantity}, Price: {Price:C}";
            }
        }
  

}
