using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TaskManagementSystem
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task(int id, string title, string description, DateTime dueDate)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nTitle: {Title}\nDescription: {Description}\nDue Date: {DueDate.ToShortDateString()}\nCompleted: {IsCompleted}\n";
        }
    }

    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        private readonly string filePath = "tasks.txt";

        public TaskManager()
        {
            LoadTasks();
        }

        // Add new task
        public void AddTask(string title, string description, DateTime dueDate)
        {
            int newId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            Task task = new Task(newId, title, description, dueDate);
            tasks.Add(task);
            SaveTasks();
        }

        // Remove task by ID
        public void RemoveTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveTasks();
            }
        }

        // Mark task as completed
        public void CompleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = true;
                SaveTasks();
            }
        }

        // Display all tasks
        public void DisplayTasks()
        {
            Console.Clear();
            foreach (var task in tasks)
            {
                Console.WriteLine(task.ToString());
            }
        }

        // Save tasks to file
        private void SaveTasks()
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    sw.WriteLine($"{task.Id}|{task.Title}|{task.Description}|{task.DueDate}|{task.IsCompleted}");
                }
            }
        }

        // Load tasks from file
        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string[] taskLines = File.ReadAllLines(filePath);
                foreach (var line in taskLines)
                {
                    string[] parts = line.Split('|');
                    Task task = new Task(
                        int.Parse(parts[0]),
                        parts[1],
                        parts[2],
                        DateTime.Parse(parts[3])
                    )
                    {
                        IsCompleted = bool.Parse(parts[4])
                    };
                    tasks.Add(task);
                }
            }
        }

        // Search tasks by keyword
        public List<Task> SearchTasks(string keyword)
        {
            return tasks.Where(t => t.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Asynchronous Task Processing (for demonstration)
        public async void ProcessTasksAsync()
        {
            Console.WriteLine("Processing tasks...");
            await Task.Run(() =>
            {
                Thread.Sleep(2000); // Simulate long-running operation
                Console.WriteLine("Tasks processed.");
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Task Management System ===");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Remove Task");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. View Tasks");
                Console.WriteLine("5. Search Task");
                Console.WriteLine("6. Process Tasks (Async)");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewTask(taskManager);
                        break;
                    case "2":
                        RemoveTask(taskManager);
                        break;
                    case "3":
                        CompleteTask(taskManager);
                        break;
                    case "4":
                        taskManager.DisplayTasks();
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "5":
                        SearchTask(taskManager);
                        break;
                    case "6":
                        taskManager.ProcessTasksAsync();
                        Console.WriteLine("Processing started asynchronously.");
                        Thread.Sleep(2000);
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddNewTask(TaskManager taskManager)
        {
            Console.Clear();
            Console.Write("Enter Task Title: ");
            string title = Console.ReadLine();
            Console.Write("Enter Task Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Task Due Date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());
            taskManager.AddTask(title, description, dueDate);
            Console.WriteLine("Task added successfully.");
            Thread.Sleep(2000);
        }

        static void RemoveTask(TaskManager taskManager)
        {
            Console.Clear();
            Console.Write("Enter Task ID to remove: ");
            int id = int.Parse(Console.ReadLine());
            taskManager.RemoveTask(id);
            Console.WriteLine("Task removed successfully.");
            Thread.Sleep(2000);
        }

        static void CompleteTask(TaskManager taskManager)
        {
            Console.Clear();
            Console.Write("Enter Task ID to mark as completed: ");
            int id = int.Parse(Console.ReadLine());
            taskManager.CompleteTask(id);
            Console.WriteLine("Task marked as completed.");
            Thread.Sleep(2000);
        }

        static void SearchTask(TaskManager taskManager)
        {
            Console.Clear();
            Console.Write("Enter keyword to search tasks: ");
            string keyword = Console.ReadLine();
            var results = taskManager.SearchTasks(keyword);
            if (results.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var task in results)
                {
                    Console.WriteLine(task.ToString());
                }
            }
            else
            {
                Console.WriteLine("No tasks found matching your search.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
