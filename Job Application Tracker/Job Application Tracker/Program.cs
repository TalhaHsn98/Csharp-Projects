using System;
using System.Collections.Generic;
using System.Linq;

namespace JobApplicationTrackerConsole
{
    // Enum for application status
    enum ApplicationStatus
    {
        Applied,
        Interview,
        Offer,
        Rejected
    }


    // Job application model class
    class JobApplication
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateApplied { get; set; }
        public ApplicationStatus Status { get; set; }
        public string Notes { get; set; }
    }

    // Main application class
    class Program
    {
        static List<JobApplication> jobApplications = new List<JobApplication>();
        static int nextId = 1;
        static int newthing = 19;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nJob Application Tracker");
                Console.WriteLine("1. Add Job Application");
                Console.WriteLine("2. View All Applications");
                Console.WriteLine("3. Edit Application");
                Console.WriteLine("4. Delete Application");
                Console.WriteLine("5. Search Application by Company Name");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddApplication();
                        break;
                    case "2":
                        ViewApplications();
                        break;
                    case "3":
                        EditApplication();
                        break;
                    case "4":
                        DeleteApplication();
                        break;
                    case "5":
                        SearchApplications();
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

        // Add a new job application
        static void AddApplication()
        {
            Console.WriteLine("\nAdding a new job application...");

            JobApplication application = new JobApplication
            {
                Id = nextId++,
                CompanyName = GetInput("Enter company name: "),
                JobTitle = GetInput("Enter job title: "),
                DateApplied = GetDate("Enter date applied (yyyy-mm-dd): "),
                Status = GetStatus(),
                Notes = GetInput("Enter any notes: ")
            };

            jobApplications.Add(application);
            Console.WriteLine("Job application added successfully.");
        }

        // View all job applications
        static void ViewApplications()
        {
            if (jobApplications.Count == 0)
            {
                Console.WriteLine("No job applications found.");
                return;
            }

            Console.WriteLine("\nAll Job Applications:");
            foreach (var application in jobApplications)
            {
                DisplayApplication(application);
            }
        }

        // Edit an existing job application
        static void EditApplication()
        {
            Console.Write("\nEnter the ID of the application to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                JobApplication application = jobApplications.FirstOrDefault(a => a.Id == id);
                if (application != null)
                {
                    application.CompanyName = GetInput("Enter new company name (leave blank to keep current): ", application.CompanyName);
                    application.JobTitle = GetInput("Enter new job title (leave blank to keep current): ", application.JobTitle);
                    application.DateApplied = GetDate("Enter new date applied (leave blank to keep current): ", application.DateApplied);
                    application.Status = GetStatus(application.Status);
                    application.Notes = GetInput("Enter new notes (leave blank to keep current): ", application.Notes);

                    Console.WriteLine("Job application updated successfully.");
                }
                else
                {
                    Console.WriteLine("Application not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        // Delete a job application
        static void DeleteApplication()
        {
            Console.Write("\nEnter the ID of the application to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                JobApplication application = jobApplications.FirstOrDefault(a => a.Id == id);
                if (application != null)
                {
                    jobApplications.Remove(application);
                    Console.WriteLine("Job application deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Application not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        // Search job applications by company name
        static void SearchApplications()
        {
            Console.Write("\nEnter the company name to search for: ");
            string companyName = Console.ReadLine();

            var results = jobApplications.Where(a => a.CompanyName.Contains(companyName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (results.Count > 0)
            {
                Console.WriteLine("\nSearch Results:");
                foreach (var application in results)
                {
                    DisplayApplication(application);
                }
            }
            else
            {
                Console.WriteLine("No job applications found for the company name.");
            }
        }

        // Utility function to display a job application
        static void DisplayApplication(JobApplication application)
        {
            Console.WriteLine($"\nID: {application.Id}");
            Console.WriteLine($"Company Name: {application.CompanyName}");
            Console.WriteLine($"Job Title: {application.JobTitle}");
            Console.WriteLine($"Date Applied: {application.DateApplied:yyyy-MM-dd}");
            Console.WriteLine($"Status: {application.Status}");
            Console.WriteLine($"Notes: {application.Notes}");
        }

        // Get user input with a prompt
        static string GetInput(string prompt, string defaultValue = null)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }

        // Get date input from the user
        static DateTime GetDate(string prompt, DateTime? defaultValue = null)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

            if (DateTime.TryParse(input, out DateTime date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Invalid date. Please try again.");
                return GetDate(prompt, defaultValue);
            }
        }

        // Get job application status
        static ApplicationStatus GetStatus(ApplicationStatus? defaultStatus = null)
        {
            Console.WriteLine("Select job application status:");
            foreach (var status in Enum.GetValues(typeof(ApplicationStatus)))
            {
                Console.WriteLine($"{(int)status} - {status}");
            }

            if (int.TryParse(Console.ReadLine(), out int statusValue) && Enum.IsDefined(typeof(ApplicationStatus), statusValue))
            {
                return (ApplicationStatus)statusValue;
            }
            else
            {
                Console.WriteLine("Invalid status. Using default.");
                return defaultStatus ?? ApplicationStatus.Applied;
            }
        }
    }
}
