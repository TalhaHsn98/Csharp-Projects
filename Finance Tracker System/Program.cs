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

        public override string ToString()
        {
            return $"[{Date}] {Category}: {Description} - Amount: {Amount}";
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
        private List<Transaction> transactions = new List<Transaction>();  // Stores all transactions
        private Dictionary<string, Category> categories = new Dictionary<string, Category>(); // Stores categories and their spending info

        private double totalIncome = 0;
        private double totalExpenses = 0;

        // Add a new transaction (income or expense)
        public void AddTransaction(double amount, string description, string category)
        {
            DateTime date = DateTime.Now;  // Timestamp the transaction

            if (!categories.ContainsKey(category))
            {
                Console.WriteLine("Category does not exist. Add the category first.");
                return;
            }

            transactions.Add(new Transaction(amount, date, description, category));  // Add the transaction

            if (amount > 0) // If it's income
            {
                totalIncome += amount;
            }
            else // If it's expense
            {
                totalExpenses += amount;
                categories[category].Spent += Math.Abs(amount);

                // Warn if the spending exceeds the category's budget
                if (categories[category].Spent > categories[category].BudgetLimit)
                {
                    Console.WriteLine($"Warning: You've exceeded the budget for {category}!");
                }
            }

            Console.WriteLine($"Transaction added: {description}, Amount: {amount}, Category: {category}");
        }

        // Create a new category and assign a budget limit
        public void AddCategory(string name, double budgetLimit)
        {
            if (categories.ContainsKey(name))
            {
                Console.WriteLine("Category already exists.");
                return;
            }

            categories.Add(name, new Category(name, budgetLimit));
            Console.WriteLine($"Category '{name}' added with a budget of {budgetLimit}");
        }

        // Display the current balance
        public void ShowBalance()
        {
            double balance = totalIncome + totalExpenses;
            Console.WriteLine($"Total Balance: ${balance}");
            Console.WriteLine($"Total Income: ${totalIncome}, Total Expenses: ${Math.Abs(totalExpenses)}");
        }

        // Show all transactions with their indices
        public void ShowTransactions()
        {
            Console.WriteLine("\n--- Transaction List ---");
            for (int i = 0; i < transactions.Count; i++)
            {
                Console.WriteLine($"{i}. {transactions[i]}");
            }
        }

        // Delete a transaction by its index
        public void DeleteTransaction(int index)
        {
            if (index < 0 || index >= transactions.Count)
            {
                Console.WriteLine("Invalid index. No transaction was deleted.");
                return;
            }

            Transaction transactionToDelete = transactions[index];
            transactions.RemoveAt(index);  // Remove the transaction

            if (transactionToDelete.Amount > 0) // If it's income, reduce total income
            {
                totalIncome -= transactionToDelete.Amount;
            }
            else // If it's an expense, reduce total expenses and adjust the category's spent amount
            {
                totalExpenses -= transactionToDelete.Amount;
                categories[transactionToDelete.Category].Spent -= Math.Abs(transactionToDelete.Amount);
            }

            Console.WriteLine($"Transaction '{transactionToDelete.Description}' was deleted.");
        }

        // Generate a report showing all income and expenses
        public void ShowReport()
        {
            Console.WriteLine("\n--- Monthly Report ---");

            Console.WriteLine($"Total Income: ${totalIncome}");
            Console.WriteLine($"Total Expenses: ${Math.Abs(totalExpenses)}");

            double savings = totalIncome + totalExpenses;
            Console.WriteLine($"Savings: ${savings}");

            Console.WriteLine("\n--- Category Breakdown ---");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Key}: Spent ${category.Value.Spent} / Budget ${category.Value.BudgetLimit}");
            }
        }
    }

    class Program
    {
        static FinanceTracker tracker = new FinanceTracker();  // Finance tracker to store all transactions

        static void Main(string[] args)
        {
            tracker.AddCategory("Rent", 1000);
            tracker.AddCategory("Groceries", 300);
            tracker.AddCategory("Entertainment", 200);

            string command = string.Empty;
            while (command != "exit")
            {
                Console.WriteLine("\nEnter a command (e.g., 'add income', 'add expense', 'add category', 'show balance', 'show transactions', 'delete transaction', 'show report', 'exit'):");
                command = Console.ReadLine().ToLower();
                ProcessCommand(command);
            }
        }

        static void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');

            if (parts.Length == 0) return;

            switch (parts[0])
            {
                case "add":
                    HandleAddCommand(parts);
                    break;
                case "show":
                    HandleShowCommand(parts);
                    break;
                case "delete":
                    HandleDeleteCommand(parts);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }

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
                    amount = -amount;
                }

                Console.Write("Enter description: ");
                string description = Console.ReadLine();

                Console.Write("Enter category: ");
                string category = Console.ReadLine();

                tracker.AddTransaction(amount, description, category);
            }
            else if (type == "category")
            {
                Console.Write("Enter category name: ");
                string category = Console.ReadLine();

                Console.Write("Enter budget limit: ");
                double limit = Convert.ToDouble(Console.ReadLine());

                tracker.AddCategory(category, limit);
            }
        }

        static void HandleShowCommand(string[] parts)
        {
            if (parts.Length < 2) return;

            string type = parts[1];

            if (type == "balance")
            {
                tracker.ShowBalance();
            }
            else if (type == "report")
            {
                tracker.ShowReport();
            }
            else if (type == "transactions")
            {
                tracker.ShowTransactions();
            }
        }

