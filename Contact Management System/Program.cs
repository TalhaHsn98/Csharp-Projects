using System;
using System.Collections.Generic;
using System.IO;

namespace ContactManagementSystem
{
    // This class represents a Contact in the system, with properties like Name, PhoneNumber, and Email.
    class Contact
    {
        public int Id { get; set; }  // The unique ID for each contact
        public string Name { get; set; }  // The name of the contact
        public string PhoneNumber { get; set; }  // The phone number of the contact
        public string Email { get; set; }  // The email address of the contact

        // Constructor to initialize a new contact with an ID, name, phone number, and email
        public Contact(int id, string name, string phoneNumber, string email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        // A method that converts the contact information into a string format for easy display
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Phone: {PhoneNumber}, Email: {Email}";
        }

        // This method will return the contact information in a more export-friendly format
        public string ToFileString()
        {
            return $"{Name},{PhoneNumber},{Email}";
        }
    }

    class Program
    {
        // A list to store the contacts. This is where all the contact information is saved in memory.
        static List<Contact> contacts = new List<Contact>();

        // A variable that holds the next available unique ID for each new contact
        static int nextId = 1;

        static void Main(string[] args)
        {
            // Boolean to keep the program running until the user decides to exit
            bool running = true;

            // Main loop that displays the menu and handles user input
            while (running)
            {
                // Display the menu options to the user
                Console.WriteLine("\nContact Management System");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View All Contacts");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Search Contact by Name");
                Console.WriteLine("6. Export Contacts to File");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                // Get user input and call the corresponding method based on their choice
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        ViewContacts();
                        break;
                    case "3":
                        EditContact();
                        break;
                    case "4":
                        DeleteContact();
                        break;
                    case "5":
                        SearchContactByName();
                        break;
                    case "6":
                        ExportContactsToFile();
                        break;
                    case "7":
                        running = false;  // Exit the program if the user chooses to
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // This method allows the user to add a new contact with a name, phone number, and email.
        static void AddContact()
        {
            Console.Write("Enter contact name: ");
            string name = Console.ReadLine();

            Console.Write("Enter contact phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter contact email: ");
            string email = Console.ReadLine();

            // Create a new Contact object and add it to the contacts list
            Contact contact = new Contact(nextId++, name, phoneNumber, email);
            contacts.Add(contact);

            Console.WriteLine("Contact added successfully.");
        }

        // This method displays all the contacts currently stored in the system.
        static void ViewContacts()
        {
            Console.WriteLine("\nAll Contacts:");
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
            }
            else
            {
                foreach (var contact in contacts)
                {
                    Console.WriteLine(contact);
                }
            }
        }

        // This method allows the user to edit an existing contact based on the contact's ID.
        static void EditContact()
        {
            Console.Write("Enter the ID of the contact to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // Find the contact with the given ID
                Contact contact = contacts.Find(c => c.Id == id);
                if (contact != null)
                {
                    // Ask the user for new details (or keep the old ones if left blank)
                    Console.Write("Enter new name (or press Enter to keep the current name): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        contact.Name = newName;
                    }

                    Console.Write("Enter new phone number (or press Enter to keep the current number): ");
                    string newPhoneNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newPhoneNumber))
                    {
                        contact.PhoneNumber = newPhoneNumber;
                    }

                    Console.Write("Enter new email (or press Enter to keep the current email): ");
                    string newEmail = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        contact.Email = newEmail;
                    }

                    Console.WriteLine("Contact updated successfully.");
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        // This method allows the user to delete a contact by ID.
        static void DeleteContact()
        {
            Console.Write("Enter the ID of the contact to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Contact contact = contacts.Find(c => c.Id == id);
                if (contact != null)
                {
                    contacts.Remove(contact);  // Remove the contact from the list
                    Console.WriteLine("Contact deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        // This method allows the user to search for contacts by name (case-insensitive).
        static void SearchContactByName()
        {
            Console.Write("Enter the name to search for: ");
            string name = Console.ReadLine();

            // Find all contacts that contain the search string in their name
            List<Contact> foundContacts = contacts.FindAll(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (foundContacts.Count > 0)
            {
                Console.WriteLine("\nFound Contacts:");
                foreach (var contact in foundContacts)
                {
                    Console.WriteLine(contact);
                }
            }
            else
            {
                Console.WriteLine("No contacts found with that name.");
            }
        }

        // New feature: Export all contacts to a file (contacts.txt).
        // This method writes all contact details to a text file in CSV format (comma-separated values).
        static void ExportContactsToFile()
        {
            string filePath = "contacts.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var contact in contacts)
                {
                    // Write each contact's details as a comma-separated line in the file
                    writer.WriteLine(contact.ToFileString());
                }
            }

            Console.WriteLine($"Contacts exported successfully to {filePath}");
        }
    }
}
