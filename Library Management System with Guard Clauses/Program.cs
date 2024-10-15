using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem
{
    // Model class representing a Book
    public class Book
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int Year { get; private set; }
        public string ISBN { get; private set; }
        public bool IsBorrowed { get; set; }

        public Book(string title, string author, int year, string isbn)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be empty.");
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Book author cannot be empty.");
            if (year < 1000 || year > DateTime.Now.Year)
                throw new ArgumentException("Invalid publication year.");
            if (string.IsNullOrWhiteSpace(isbn) || isbn.Length != 13)
                throw new ArgumentException("ISBN must be exactly 13 characters long.");

            Title = title;
            Author = author;
            Year = year;
            ISBN = isbn;
            IsBorrowed = false;
        }
    }

    // Model class representing a User
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        public User(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("User name cannot be empty.");
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                throw new ArgumentException("Invalid email address.");

            Name = name;
            Email = email;
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }

    // Service class for Library Management
    public class LibraryService
    {
        private readonly LibraryRepository _libraryRepository;

        public LibraryService()
        {
            _libraryRepository = new LibraryRepository();
        }

        // Register a user
        public void RegisterUser(string name, string email)
        {
            try
            {
                var user = new User(name, email);
                _libraryRepository.AddUser(user);
                Console.WriteLine($"User {name} registered successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Add a book to the library
        public void AddBook(string title, string author, int year, string isbn)
        {
            try
            {
                var book = new Book(title, author, year, isbn);
                _libraryRepository.AddBook(book);
                Console.WriteLine($"Book '{title}' added successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Borrow a book
        public void BorrowBook(string userEmail, string isbn)
        {
            var user = _libraryRepository.GetUserByEmail(userEmail);
            if (user == null)
            {
                Console.WriteLine("Error: User not found.");
                return;
            }

            var book = _libraryRepository.GetBookByISBN(isbn);
            if (book == null)
            {
                Console.WriteLine("Error: Book not found.");
                return;
            }

            if (book.IsBorrowed)
            {
                Console.WriteLine("Error: Book is already borrowed.");
                return;
            }

            book.IsBorrowed = true;
            Console.WriteLine($"Book '{book.Title}' borrowed successfully by {user.Name}.");
        }
    }

    // Repository class for storing books and users
    public class LibraryRepository
    {
        private readonly List<Book> _books;
        private readonly List<User> _users;

        public LibraryRepository()
        {
            _books = new List<Book>();
            _users = new List<User>();
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public Book GetBookByISBN(string isbn)
        {
            return _books.Find(b => b.ISBN == isbn);
        }

        public User GetUserByEmail(string email)
        {
            return _users.Find(u => u.Email == email);
        }
    }

    // Main entry point
    class Program
    {
        static void Main(string[] args)
        {
            var libraryService = new LibraryService();

            // Register Users
            libraryService.RegisterUser("Alice", "alice@example.com");
            libraryService.RegisterUser("Bob", "bob@example.com");

            // Add Books
            libraryService.AddBook("The Catcher in the Rye", "J.D. Salinger", 1951, "1234567890123");
            libraryService.AddBook("To Kill a Mockingbird", "Harper Lee", 1960, "1234567890124");

            // Borrow Books
            libraryService.BorrowBook("alice@example.com", "1234567890123");
            libraryService.BorrowBook("bob@example.com", "1234567890123"); // Should throw an error, book already borrowed
        }
    }
}
