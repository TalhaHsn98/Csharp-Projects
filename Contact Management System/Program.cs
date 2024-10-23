using System;
using System.Collections.Generic;

namespace ContactManagementSystem
{
    class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Contact(int id, string name, string phoneNumber, string email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Phone: {PhoneNumber}, Email: {Email}";
        }
    }

    class Program
    {
        static List<Contact> contacts = new List<Contact>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nContact Management System");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View All Contacts");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Search Contact by Name");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

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
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddContact()
        {
            Console.Write("Enter contact name: ");
            string name = Console.ReadLine();

            Console.Write("Enter contact phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter contact email: ");
            string email = Console.ReadLine();

            Contact contact = new Contact(nextId++, name, phoneNumber, email);
            contacts.Add(contact);

            Console.WriteLine("Contact added successfully.");
        }

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

        static void EditContact()
        {
            Console.Write("Enter the ID of the contact to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Contact contact = contacts.Find(c => c.Id == id);
                if (contact != null)
                {
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

        static void DeleteContact()
        {
            Console.Write("Enter the ID of the contact to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Contact contact = contacts.Find(c => c.Id == id);
                if (contact != null)
                {
                    contacts.Remove(contact);
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

        static void SearchContactByName()
        {
            Console.Write("Enter the name to search for: ");
            string name = Console.ReadLine();

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
    }
}
