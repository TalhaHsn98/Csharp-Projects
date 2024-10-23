using System;
using System.Collections.Generic;

namespace PizzaShop
{
    // Enum for Pizza Sizes - This lets me easily manage different pizza sizes
    enum PizzaSize
    {
        Small,
        Medium,
        Large
    }

    // Enum for Pizza Toppings - I use this to list all possible toppings a customer can choose
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

    // Pizza class handles individual pizza details
    class Pizza
    {
        public PizzaSize Size { get; set; } // Storing the pizza size here
        public List<PizzaToppings> Toppings { get; set; } // List to hold all the chosen toppings
        public double BasePrice { get; set; } // The base price changes based on pizza size

        // This constructor initializes the pizza with a size and sets the base price
        public Pizza(PizzaSize size)
        {
            Size = size;
            Toppings = new List<PizzaToppings>(); // We start with no toppings
            BasePrice = CalculateBasePrice(); // Calculate the base price for the pizza
        }

        // This method calculates the base price based on the size of the pizza

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

        // This method adds up the total cost of the pizza including the toppings
        public double CalculateTotalCost()
        {
            double toppingCost = 0.50 * Toppings.Count; // Each topping costs $0.50
            double totalCost = BasePrice + toppingCost;

            Console.WriteLine($"Your cost for the pizza: ${totalCost}");
            return totalCost;
        }



        // Method to add toppings to the pizza - just adds them to the list
        public void AddTopping(PizzaToppings topping)
        {
            Toppings.Add(topping);
        }

        // This method displays the pizza details: size, toppings, and total cost
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

        // New method to clear the toppings from the pizza - in case the customer changes their mind
        public void ClearToppings()
        {
            Toppings.Clear();
            Console.WriteLine("All toppings have been removed.");
        }
    }

    // Order class to handle the entire order with multiple pizzas
    class Order
    {
        public List<Pizza> Pizzas { get; private set; } // List to hold all pizzas in the order
        public double TotalCost { get; private set; } // Track the total cost of the order

        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        // Method to add a pizza to the order - I wrote this to update the total cost as well
        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
            TotalCost += pizza.CalculateTotalCost();
        }

        // This method displays the entire order, showing details for each pizza
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

        // New method to remove the last pizza added - helps in case someone changes their mind
        public void RemoveLastPizza()
        {
            if (Pizzas.Count > 0)
            {
                var lastPizza = Pizzas[Pizzas.Count - 1];
                TotalCost -= lastPizza.CalculateTotalCost();
                Pizzas.RemoveAt(Pizzas.Count - 1);
                Console.WriteLine("Last pizza removed from your order.");
            }
            else
            {
                Console.WriteLine("No pizzas to remove.");
            }
        }

        // Method to clear the entire order, resetting everything - useful for starting fresh
        public void ClearOrder()
        {
            Pizzas.Clear();
            TotalCost = 0;
            Console.WriteLine("Your order has been cleared.");
        }
    }

    // Menu class to handle all user interactions
    static class Menu
    {
        // I wrote this method to allow the user to select the pizza size from a list
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

        // This method allows users to select toppings for their pizza
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

        // Show main menu - I wrote this to display the user's options
        public static void ShowMainMenu()
        {
            Console.WriteLine("\nWelcome to Pizza Shop!");
            Console.WriteLine("1. Order Pizza");
            Console.WriteLine("2. Remove Last Pizza");
            Console.WriteLine("3. Clear Entire Order");
            Console.WriteLine("4. Finalize and Exit");
        }

        // This method finalizes the order and displays it - it's the final step before the user exits
        public static void FinalizeOrder(Order order)
        {
            Console.WriteLine("\nThank you for your order!");
            order.DisplayOrder();
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }

    // Main class to handle the application logic - I used a loop here to keep the program running until the user finalizes their order
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            Order currentOrder = new Order(); // This is where the order gets stored

            while (isRunning)
            {
                Menu.ShowMainMenu(); // Show the user their options
                switch (Console.ReadLine())
                {
                    case "1":
                        PizzaSize size = Menu.ChoosePizzaSize(); // User selects a size
                        Pizza pizza = new Pizza(size); // A new pizza is created based on the size
                        Menu.ChooseToppings(pizza); // User chooses toppings for their pizza
                        currentOrder.AddPizza(pizza); // Add pizza to the order
                        break;
                    case "2":
                        currentOrder.RemoveLastPizza(); // Remove the last pizza added
                        break;
                    case "3":
                        currentOrder.ClearOrder(); // Clears the entire order
                        break;
                    case "4":
                        Menu.FinalizeOrder(currentOrder); // Finalize the order and exit
                        isRunning = false; // Ends the loop
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again."); // Error handling for invalid input
                        break;
                }
            }
        }
    }
}
