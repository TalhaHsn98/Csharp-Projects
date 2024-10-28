using System;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }

    public Product(int id, string name, decimal price, int quantity, string description)
    {
        ProductID = id;
        Name = name;
        Price = price;
        Quantity = quantity;
        Description = description;
    }

    public override string ToString()
    {
        return $"ID: {ProductID}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}, Description: {Description}";
    }
}
