using System;
using System.Collections.Generic;

namespace TaskManagerApp
{
    class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public Task(int id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
        }

        public void MarkAsComplete()
        {
            IsCompleted = true;
        }

        public override string ToString()
        {
            return $"{Id}: {Description} - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }

    class Program
    {
        static List<Task> tasks = new List<Task>();
        static int nextTaskId = 1;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nTask Manager");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Mark Task as Completed");
                Console.WriteLine("3. View All Tasks");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        CompleteTask();
                        break;
                    case "3":
                        ViewTasks();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();
            Task task = new Task(nextTaskId++, description);
            tasks.Add(task);
            Console.WriteLine("Task added successfully.");
        }

        static void CompleteTask()
        {
            Console.Write("Enter task ID to mark as complete: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.Find(t => t.Id == taskId);
                if (task != null)
                {
                    task.MarkAsComplete();
                    Console.WriteLine("Task marked as complete.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }

        static void ViewTasks()
        {
            Console.WriteLine("\nAll Tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        static void DeleteTask()
        {
            Console.Write("Enter task ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.Find(t => t.Id == taskId);
                if (task != null)
                {
                    tasks.Remove(task);
                    Console.WriteLine("Task deleted.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID.");
            }
        }
    }
}
