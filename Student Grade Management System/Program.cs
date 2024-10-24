using System;
using System.Collections.Generic;

namespace StudentGradeManagementSystem
{
    // Represents a student and stores information about their grades in different subjects
    class Student
    {
        public int Id { get; set; }   // Unique ID for each student
        public string Name { get; set; }  // Name of the student
        public Dictionary<string, List<int>> Grades { get; set; }  // A dictionary to store grades for each subject

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
            Grades = new Dictionary<string, List<int>>();  // Initialize the grades dictionary
        }

        // Adds a grade for a specific subject
        public void AddGrade(string subject, int grade)
        {
            if (!Grades.ContainsKey(subject))
            {
                Grades[subject] = new List<int>();
            }
            Grades[subject].Add(grade);
        }

        // Returns the average grade for a specific subject
        public double GetAverageGrade(string subject)
        {
            if (Grades.ContainsKey(subject) && Grades[subject].Count > 0)
            {
                int total = 0;
                foreach (int grade in Grades[subject])
                {
                    total += grade;
                }
                return total / (double)Grades[subject].Count;
            }
            return 0;
        }

        // Returns the highest grade for a specific subject
        public int GetHighestGrade(string subject)
        {
            if (Grades.ContainsKey(subject) && Grades[subject].Count > 0)
            {
                return Grades[subject].Max();
            }
            return 0;
        }

        // Returns the lowest grade for a specific subject
        public int GetLowestGrade(string subject)
        {
            if (Grades.ContainsKey(subject) && Grades[subject].Count > 0)
            {
                return Grades[subject].Min();
            }
            return 0;
        }

        // Calculates the overall average across all subjects
        public double GetOverallAverage()
        {
            double totalSum = 0;
            int count = 0;

            foreach (var subjectGrades in Grades.Values)
            {
                totalSum += subjectGrades.Sum();
                count += subjectGrades.Count;
            }

            if (count == 0) return 0;
            return totalSum / count;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}";
        }
    }

    // Manages a collection of students and handles operations like adding students, assigning grades, etc.
    class GradeManager
    {
        private List<Student> students = new List<Student>();
        private int nextStudentId = 1;

        // Adds a new student to the system
        public void AddStudent(string name)
        {
            Student student = new Student(nextStudentId++, name);
            students.Add(student);
            Console.WriteLine($"Student {name} added with ID {student.Id}.");
        }

        // Finds a student by ID
        private Student FindStudentById(int id)
        {
            return students.Find(s => s.Id == id);
        }

        // Allows the user to assign grades to a student in a specific subject
        public void AssignGrade(int studentId, string subject, int grade)
        {
            Student student = FindStudentById(studentId);
            if (student != null)
            {
                student.AddGrade(subject, grade);
                Console.WriteLine($"Grade {grade} added in {subject} for student {student.Name}.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Displays the grades of a student for all subjects
        public void ViewGrades(int studentId)
        {
            Student student = FindStudentById(studentId);
            if (student != null)
            {
                Console.WriteLine($"\nGrades for {student.Name}:");
                foreach (var subject in student.Grades.Keys)
                {
                    Console.WriteLine($"\nSubject: {subject}");
                    Console.WriteLine($"Grades: {string.Join(", ", student.Grades[subject])}");
                    Console.WriteLine($"Average: {student.GetAverageGrade(subject)}");
                    Console.WriteLine($"Highest Grade: {student.GetHighestGrade(subject)}");
                    Console.WriteLine($"Lowest Grade: {student.GetLowestGrade(subject)}");
                }
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Displays the overall average grade for a student
        public void DisplayOverallAverage(int studentId)
        {
            Student student = FindStudentById(studentId);
            if (student != null)
            {
                Console.WriteLine($"\nOverall Average for {student.Name}: {student.GetOverallAverage():F2}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }

        // Searches for students by name and displays their information
        public void SearchStudentByName(string name)
        {
            var foundStudents = students.FindAll(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (foundStudents.Count > 0)
            {
                Console.WriteLine("\nFound Students:");
                foreach (var student in foundStudents)
                {
                    Console.WriteLine(student);
                }
            }
            else
            {
                Console.WriteLine("No students found with that name.");
            }
        }

        // Displays all students in the system
        public void DisplayAllStudents()
        {
            if (students.Count > 0)
            {
                Console.WriteLine("\nAll Students:");
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }
            else
            {
                Console.WriteLine("No students found.");
            }
        }
    }

    // Main program to interact with the user and manage students and grades
    class Program
    {
        static void Main(string[] args)
        {
            GradeManager gradeManager = new GradeManager();
            bool running = true;

            while (running)
            {
                // Display menu options for the user
                Console.WriteLine("\nStudent Grade Management System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Assign Grade");
                Console.WriteLine("3. View Grades");
                Console.WriteLine("4. Display Overall Average");
                Console.WriteLine("5. Search for Student by Name");
                Console.WriteLine("6. Display All Students");
                Console.WriteLine("7. Exit");
                Console.Write("Select an option: ");
                string input = Console.ReadLine();

                // Execute the corresponding action based on the user's choice
                switch (input)
                {
                    case "1":
                        Console.Write("Enter student name: ");
                        string name = Console.ReadLine();
                        gradeManager.AddStudent(name);
                        break;

                    case "2":
                        Console.Write("Enter student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int studentId1))
                        {
                            Console.Write("Enter subject: ");
                            string subject = Console.ReadLine();
                            Console.Write("Enter grade: ");
                            if (int.TryParse(Console.ReadLine(), out int grade))
                            {
                                gradeManager.AssignGrade(studentId1, subject, grade);
                            }
                            else
                            {
                                Console.WriteLine("Invalid grade.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid student ID.");
                        }
                        break;

                    case "3":
                        Console.Write("Enter student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int studentId2))
                        {
                            gradeManager.ViewGrades(studentId2);
                        }
                        else
                        {
                            Console.WriteLine("Invalid student ID.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter student ID: ");
                        if (int.TryParse(Console.ReadLine(), out int studentId3))
                        {
                            gradeManager.DisplayOverallAverage(studentId3);
                        }
                        else
                        {
                            Console.WriteLine("Invalid student ID.");
                        }
                        break;

                    case "5":
                        Console.Write("Enter student name to search: ");
                        string searchName = Console.ReadLine();
                        gradeManager.SearchStudentByName(searchName);
                        break;

                    case "6":
                        gradeManager.DisplayAllStudents();
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
