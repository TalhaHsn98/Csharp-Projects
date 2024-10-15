/*internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, I am the best Programmer from now on");
    }
}
*/


namespace BankManagementSystem
{
    class BankAccount
    {
        public int AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        public decimal Balance { get; private set; }

        public BankAccount(int accountNumber, string accountHolderName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive.");
                return;
            }

            Balance += amount;
            Console.WriteLine($"Deposited {amount:C} to account {AccountNumber}. New balance: {Balance:C}");
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be positive.");
                return;
            }

            if (amount > Balance)
            {
                Console.WriteLine("Insufficient balance.");
                return;
            }

            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:C} from account {AccountNumber}. New balance: {Balance:C}");
        }

        public void DisplayAccountInfo()
        {
            Console.WriteLine($"\nAccount Number: {AccountNumber}\nAccount Holder: {AccountHolderName}\nBalance: {Balance:C}\n");
        }
    }

    class Bank
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private int nextAccountNumber = 1001;

        public void CreateAccount(string accountHolderName, decimal initialBalance)
        {
            BankAccount newAccount = new BankAccount(nextAccountNumber++, accountHolderName, initialBalance);
            accounts.Add(newAccount);
            Console.WriteLine($"Account created successfully! Account Number: {newAccount.AccountNumber}");
        }

        public void DepositToAccount(int accountNumber, decimal amount)
        {
            BankAccount account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
            }
        }

        public void WithdrawFromAccount(int accountNumber, decimal amount)
        {
            BankAccount account = FindAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount);
            }
        }

        public void DisplayAccountDetails(int accountNumber)
        {
            BankAccount account = FindAccount(accountNumber);
            if (account != null)
            {
                account.DisplayAccountInfo();
            }
        }

        private BankAccount FindAccount(int accountNumber)
        {
            foreach (var account in accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }

            Console.WriteLine($"Account {accountNumber} not found.");
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n--- Bank Management System ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter account holder's name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter initial deposit amount: ");
                        decimal initialDeposit = decimal.Parse(Console.ReadLine());
                        bank.CreateAccount(name, initialDeposit);
                        break;

                    case "2":
                        Console.Write("Enter account number: ");
                        int depositAccountNumber = int.Parse(Console.ReadLine());
                        Console.Write("Enter deposit amount: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        bank.DepositToAccount(depositAccountNumber, depositAmount);
                        break;

                    case "3":
                        Console.Write("Enter account number: ");
                        int withdrawAccountNumber = int.Parse(Console.ReadLine());
                        Console.Write("Enter withdrawal amount: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        bank.WithdrawFromAccount(withdrawAccountNumber, withdrawAmount);
                        break;

                    case "4":
                        Console.Write("Enter account number: ");
                        int displayAccountNumber = int.Parse(Console.ReadLine());
                        bank.DisplayAccountDetails(displayAccountNumber);
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
