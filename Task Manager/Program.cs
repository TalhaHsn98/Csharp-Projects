using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagerApp
{
    enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    class Task
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }

        public Task(int id, string description, DateTime dueDate, TaskPriority priority)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
            DueDate = dueDate;
            Priority = priority;
        }

        public void MarkAsComplete()
        {
            IsCompleted = true;
        }

        public void EditTask(string newDescription, DateTime newDueDate, TaskPriority newPriority)
        {
            Description = newDescription;
            DueDate = newDueDate;
            Priority = newPriority;
        }

        public override string ToString()
        {
            return $"{Id}: {Description} - {(IsCompleted ? "Completed" : "Pending")} - Due: {DueDate.ToShortDateString()} - Priority: {Priority}";
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
                Console.WriteLine("4. Edit Task");
                Console.WriteLine("5. Clear Completed Tasks");
                Console.WriteLine("6. View Tasks Sorted by Priority");
                Console.WriteLine("7. Exit");
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
                        EditTask();
                        break;
                    case "5":
                        ClearCompletedTasks();
                        break;
                    case "6":
                        ViewTasksByPriority();
                        break;
                    case "7":
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

            Console.Write("Enter task due date (yyyy-mm-dd): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Select task priority (1 - Low, 2 - Medium, 3 - High): ");
            TaskPriority priority = (TaskPriority)(int.Parse(Console.ReadLine()) - 1);

            Task task = new Task(nextTaskId++, description, dueDate, priority);
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

        static void EditTask()
        {
            Console.Write("Enter task ID to edit: ");
            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.Find(t => t.Id == taskId);
                if (task != null)
                {
                    Console.Write("Enter new description: ");
                    string newDescription = Console.ReadLine();

                    Console.Write("Enter new due date (yyyy-mm-dd): ");
                    DateTime newDueDate = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Select new task priority (1 - Low, 2 - Medium, 3 - High): ");
                    TaskPriority newPriority = (TaskPriority)(int.Parse(Console.ReadLine()) - 1);

                    task.EditTask(newDescription, newDueDate, newPriority);
                    Console.WriteLine("Task updated successfully.");
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

        static void ClearCompletedTasks()
        {
            tasks.RemoveAll(t => t.IsCompleted);
            Console.WriteLine("All completed tasks cleared.");
        }

        static void ViewTasksByPriority()
        {
            Console.WriteLine("\nTasks Sorted by Priority:");
            var sortedTasks = tasks.OrderByDescending(t => t.Priority).ToList();
            foreach (var task in sortedTasks)
            {
                Console.WriteLine(task);
            }
        }
    }
}
