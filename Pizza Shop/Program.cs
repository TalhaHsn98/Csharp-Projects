using System;
using System.Collections.Generic;

namespace PizzaShop
{
    // Enum for Pizza Sizes
    enum PizzaSize
    {
        Small,
        Medium,
        Large
    }

    // Enum for Pizza Toppings
    enum PizzaToppings
    {
        Cheese,
        Pepperoni,
        Mushrooms,
        Onions,
        Sausage,
        Bacon,
        ExtraCheese,
        GreenPeppers,
        Pineapple
    }

    // Pizza class to manage pizza properties
    class Pizza
    {
        public PizzaSize Size { get; set; }
        public List<PizzaToppings> Toppings { get; set; }
        public double BasePrice { get; set; }

        public Pizza(PizzaSize size)
        {
            Size = size;
            Toppings = new List<PizzaToppings>();
            BasePrice = CalculateBasePrice();
        }

        private double CalculateBasePrice()
        {
            switch (Size)
            {
                case PizzaSize.Small: return 5.00;
                case PizzaSize.Medium: return 7.00;
                case PizzaSize.Large: return 9.00;
                default: return 0;
            }
        }

        public double CalculateTotalCost()
        {
            double toppingCost = 0.50 * Toppings.Count;
            return BasePrice + toppingCost;
        }

        public void AddTopping(PizzaToppings topping)
        {
            Toppings.Add(topping);
        }

        public void DisplayPizza()
        {
            Console.WriteLine($"Pizza Size: {Size}");
            Console.WriteLine("Toppings: ");
            foreach (var topping in Toppings)
            {
                Console.WriteLine($"- {topping}");
            }
            Console.WriteLine($"Total Cost: ${CalculateTotalCost():0.00}");
        }
    }

    // Order class to manage multiple pizzas
    class Order
    {
        public List<Pizza> Pizzas { get; private set; }
        public double TotalCost { get; private set; }

        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
            TotalCost += pizza.CalculateTotalCost();
        }

        public void DisplayOrder()
        {
            Console.WriteLine("\nYour Pizza Order:");
            foreach (var pizza in Pizzas)
            {
                pizza.DisplayPizza();
                Console.WriteLine();
            }
            Console.WriteLine($"Total Order Cost: ${TotalCost:0.00}");
        }
    }

    // Menu for user interaction
    static class Menu
    {
        public static PizzaSize ChoosePizzaSize()
        {
            Console.WriteLine("\nChoose pizza size:");
            Console.WriteLine("1. Small ($5.00)");
            Console.WriteLine("2. Medium ($7.00)");
            Console.WriteLine("3. Large ($9.00)");

            switch (Console.ReadLine())
            {
                case "1": return PizzaSize.Small;
                case "2": return PizzaSize.Medium;
                case "3": return PizzaSize.Large;
                default: return PizzaSize.Small;
            }
        }

        public static void ChooseToppings(Pizza pizza)
        {
            Console.WriteLine("\nChoose your toppings (Type the number and press Enter after each, type '0' to finish):");
            Console.WriteLine("1. Cheese");
            Console.WriteLine("2. Pepperoni");
            Console.WriteLine("3. Mushrooms");
            Console.WriteLine("4. Onions");
            Console.WriteLine("5. Sausage");
            Console.WriteLine("6. Bacon");
            Console.WriteLine("7. Extra Cheese");
            Console.WriteLine("8. Green Peppers");
            Console.WriteLine("9. Pineapple");

            bool choosingToppings = true;
            while (choosingToppings)
            {
                switch (Console.ReadLine())
                {
                    case "1": pizza.AddTopping(PizzaToppings.Cheese); break;
                    case "2": pizza.AddTopping(PizzaToppings.Pepperoni); break;
                    case "3": pizza.AddTopping(PizzaToppings.Mushrooms); break;
                    case "4": pizza.AddTopping(PizzaToppings.Onions); break;
                    case "5": pizza.AddTopping(PizzaToppings.Sausage); break;
                    case "6": pizza.AddTopping(PizzaToppings.Bacon); break;
                    case "7": pizza.AddTopping(PizzaToppings.ExtraCheese); break;
                    case "8": pizza.AddTopping(PizzaToppings.GreenPeppers); break;
                    case "9": pizza.AddTopping(PizzaToppings.Pineapple); break;
                    case "0": choosingToppings = false; break;
                    default: Console.WriteLine("Invalid option, try again."); break;
                }
            }
        }

        public static void ShowMainMenu()
        {
            Console.WriteLine("\nWelcome to Pizza Shop!");
            Console.WriteLine("1. Order Pizza");
            Console.WriteLine("2. Exit");
        }

        public static void FinalizeOrder(Order order)
        {
            Console.WriteLine("\nThank you for your order!");
            order.DisplayOrder();
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }

    // Main class to drive the application
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            Order currentOrder = new Order();

            while (isRunning)
            {
                Menu.ShowMainMenu();
                switch (Console.ReadLine())
                {
                    case "1":
                        PizzaSize size = Menu.ChoosePizzaSize();
                        Pizza pizza = new Pizza(size);
                        Menu.ChooseToppings(pizza);
                        currentOrder.AddPizza(pizza);
                        break;
                    case "2":
                        Menu.FinalizeOrder(currentOrder);
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
