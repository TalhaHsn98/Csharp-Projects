using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalFinanceTracker
{
    // Represents a single transaction (either income or expense)
    class Transaction
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public Transaction(double amount, DateTime date, string description, string category)
        {
            Amount = amount;
            Date = date;
            Description = description;
            Category = category;
        }
    }

    // Represents a budget category (e.g., Rent, Groceries)
    class Category
    {
        public string Name { get; set; }
        public double BudgetLimit { get; set; }
        public double Spent { get; set; }

        public Category(string name, double budgetLimit)
        {
            Name = name;
            BudgetLimit = budgetLimit;
            Spent = 0; // Initially, nothing is spent
        }
    }

    class FinanceTracker
    {
        // List to store all transactions
        private List<Transaction> transactions = new List<Transaction>();

        // Dictionary to keep track of each category's budget and spending
        private Dictionary<string, Category> categories = new Dictionary<string, Category>();

        // These track the total income and total expenses separately
        private double totalIncome = 0;
        private double totalExpenses = 0;

        // Add a new transaction (income or expense)
        public void AddTransaction(double amount, string description, string category)
        {
            DateTime date = DateTime.Now;  // The current date/time is assigned when the transaction is logged

            // Ensure the category exists before adding a transaction to it
            if (!categories.ContainsKey(category))
            {
                Console.WriteLine("Category does not exist. Add the category first.");
                return;
            }

            // Add the transaction to the list
            transactions.Add(new Transaction(amount, date, description, category));

            // Check if it's income (positive amount) or expense (negative)
            if (amount > 0)
            {
                totalIncome += amount;
            }
            else
            {
                totalExpenses += amount;
                categories[category].Spent += Math.Abs(amount); // Add the expense to the category's spending

                // Warn if the spending in a category exceeds the budget
                if (categories[category].Spent > categories[category].BudgetLimit)
                {
                    Console.WriteLine($"Warning: You've exceeded the budget for {category}!");
                }
            }

            Console.WriteLine($"Transaction added: {description}, Amount: {amount}, Category: {category}");
        }

        // Create a new category and assign a budget limit to it
        public void AddCategory(string name, double budgetLimit)
        {
            // Check if the category already exists
            if (categories.ContainsKey(name))
            {
                Console.WriteLine("Category already exists.");
                return;
            }

            // If not, create it and add it to the dictionary
            categories.Add(name, new Category(name, budgetLimit));
            Console.WriteLine($"Category '{name}' added with a budget of {budgetLimit}");
        }

        // Display the current balance, income, and expenses
        public void ShowBalance()
        {
            double balance = totalIncome + totalExpenses;  // Net balance is income minus expenses
            Console.WriteLine($"Total Balance: ${balance}");
            Console.WriteLine($"Total Income: ${totalIncome}, Total Expenses: ${Math.Abs(totalExpenses)}");
        }

        // Generate a monthly report, showing income, expenses, savings, and category breakdowns
        public void ShowReport()
        {
            Console.WriteLine("\n--- Monthly Report ---");

            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expenses: ${Math.Abs(totalExpenses)}");

            double savings = totalIncome + totalExpenses;  // Remaining amount is treated as savings
            Console.WriteLine($"Savings: ${savings}");

            Console.WriteLine("\n--- Category Breakdown ---");
            // Iterate through the categories and show how much was spent in each
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Key}: Spent ${category.Value.Spent} / Budget ${category.Value.BudgetLimit}");
            }
        }
    }

    class Program
    {
        // Instantiate the finance tracker, which holds all financial data and operations
        static FinanceTracker tracker = new FinanceTracker();

        static void Main(string[] args)
        {
            // Adding some default categories for convenience
            tracker.AddCategory("Rent", 1000);
            tracker.AddCategory("Groceries", 300);
            tracker.AddCategory("Entertainment", 200);

            string command = string.Empty;
            // Keep asking for user input until they type 'exit'
            while (command != "exit")
            {
                Console.WriteLine("\nEnter a command (e.g., 'add income', 'add expense', 'add category', 'show balance', 'show report', 'exit'):");
                command = Console.ReadLine().ToLower();  // Convert input to lowercase for uniform processing
                ProcessCommand(command);
            }
        }

        // Process the user's command and call the appropriate function
        static void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');  // Split command input into parts (e.g., 'add income')

            if (parts.Length == 0) return;

            switch (parts[0])
            {
                case "add":
                    HandleAddCommand(parts);
                    break;
                case "show":
                    HandleShowCommand(parts);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

        // Handle 'add' commands (e.g., 'add income', 'add category')
        static void HandleAddCommand(string[] parts)
        {
            if (parts.Length < 2) return;

            string type = parts[1];

            if (type == "income" || type == "expense")
            {
                Console.Write("Enter amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());

                if (type == "expense")
                {
                    amount = -amount;  // Negative amount for expenses
                }

                Console.Write("Enter description: ");
                string description = Console.ReadLine();

                Console.Write("Enter category: ");
                string category = Console.ReadLine();

                tracker.AddTransaction(amount, description, category);  // Add the transaction
            }
            else if (type == "category")
            {
                Console.Write("Enter category name: ");
                string category = Console.ReadLine();

                Console.Write("Enter budget limit: ");
                double limit = Convert.ToDouble(Console.ReadLine());

                tracker.AddCategory(category, limit);  // Add the new category
            }
        }

        // Handle 'show' commands (e.g., 'show balance', 'show report')
        static void HandleShowCommand(string[] parts)
        {
            if (parts.Length < 2) return;

            string type = parts[1];

            if (type == "balance")
            {
                tracker.ShowBalance();  // Show balance information
            }
            else if (type == "report")
            {
                tracker.ShowReport();  // Show the financial report
            }
        }
    }
}
