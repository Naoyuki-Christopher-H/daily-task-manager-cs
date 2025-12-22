using System;
using System.Collections.Generic;
using System.Linq;
using daily_task_manager_cs.Models;
using daily_task_manager_cs.Utils;

namespace daily_task_manager_cs.Managers
{
    /// <summary>
    /// Manages task operations including CRUD operations and task organization.
    /// Handles task creation, modification, deletion, and display.
    /// </summary>
    public class TaskManager
    {
        /// <summary>
        /// Currently logged in user
        /// </summary>
        private User currentUser;

        /// <summary>
        /// JSON data manager for persistence
        /// </summary>
        private JsonDataManager jsonDataManager;

        /// <summary>
        /// Application data reference
        /// </summary>
        private AppData appData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user">Current user</param>
        /// <param name="dataManager">JSON data manager</param>
        /// <param name="data">Application data</param>
        public TaskManager(User user, JsonDataManager dataManager, AppData data)
        {
            currentUser = user;
            jsonDataManager = dataManager;
            appData = data;
        }

        /// <summary>
        /// Displays the main task management menu
        /// </summary>
        public void ShowTaskMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("TASK MANAGER");
                Console.WriteLine($"Welcome, {currentUser.Username}!\n");
                Console.WriteLine("===================================");
                Console.WriteLine("1. Add a new task");
                Console.WriteLine("2. Remove a task");
                Console.WriteLine("3. Mark a task as complete");
                Console.WriteLine("4. List all tasks");
                Console.WriteLine("5. Edit a task");
                Console.WriteLine("6. Search tasks");
                Console.WriteLine("7. Logout");
                Console.WriteLine("8. Exit application");
                Console.Write("\nEnter your choice (1-8): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        RemoveTask();
                        break;
                    case "3":
                        MarkTaskComplete();
                        break;
                    case "4":
                        ListTasks();
                        break;
                    case "5":
                        EditTask();
                        break;
                    case "6":
                        SearchTasks();
                        break;
                    case "7":
                        return; // Logout
                    case "8":
                        ExitApplication();
                        break;
                    default:
                        ConsoleHelper.DisplayError("Invalid choice. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Adds a new task for the current user
        /// </summary>
        private void AddTask()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("ADD NEW TASK");
            Console.WriteLine("===================================\n");

            // Get task title
            string title = ConsoleHelper.GetStringInput("Enter task title: ");

            // Get due date (optional)
            DateTime? dueDate = ConsoleHelper.GetDateInput("Enter due date (YYYY-MM-DD) OR press Enter to skip: ");

            // Get priority
            Priority priority = ConsoleHelper.GetPriorityInput();

            // Generate new task ID
            int newId = currentUser.Tasks.Count > 0
                ? currentUser.Tasks.Max(t => t.Id) + 1
                : 1;

            // Create new task
            TaskItem newTask = new TaskItem(newId, title, dueDate, priority);

            // Add to user's tasks
            currentUser.Tasks.Add(newTask);

            // Save to JSON
            jsonDataManager.SaveData(appData);

            ConsoleHelper.DisplaySuccess($"Task added successfully! (ID: {newId})");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Removes a task by ID
        /// </summary>
        private void RemoveTask()
        {
            if (!currentUser.Tasks.Any())
            {
                ConsoleHelper.DisplayInfo("No tasks available to remove.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("REMOVE TASK");
            Console.WriteLine("===================================\n");
            ListTasksSimple();

            int taskId = ConsoleHelper.GetIntInput("\nEnter task ID to remove: ", 1);

            TaskItem taskToRemove = currentUser.Tasks.Find(t => t.Id == taskId);

            if (taskToRemove == null)
            {
                ConsoleHelper.DisplayError($"Task with ID {taskId} not found.");
            }
            else
            {
                currentUser.Tasks.Remove(taskToRemove);
                jsonDataManager.SaveData(appData);
                ConsoleHelper.DisplaySuccess($"Task removed successfully! (ID: {taskId})");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Marks a task as complete by ID
        /// </summary>
        private void MarkTaskComplete()
        {
            if (!currentUser.Tasks.Any())
            {
                ConsoleHelper.DisplayInfo("No tasks available.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("MARK TASK COMPLETE");
            Console.WriteLine("===================================\n");
            ListTasksSimple();

            int taskId = ConsoleHelper.GetIntInput("\nEnter task ID to mark as complete: ", 1);

            TaskItem taskToComplete = currentUser.Tasks.Find(t => t.Id == taskId);

            if (taskToComplete == null)
            {
                ConsoleHelper.DisplayError($"Task with ID {taskId} not found.");
            }
            else if (taskToComplete.IsComplete)
            {
                ConsoleHelper.DisplayInfo($"Task with ID {taskId} is already complete.");
            }
            else
            {
                taskToComplete.IsComplete = true;
                jsonDataManager.SaveData(appData);
                ConsoleHelper.DisplaySuccess($"Task marked as complete! (ID: {taskId})");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lists all tasks with sorting and filtering options
        /// </summary>
        private void ListTasks()
        {
            if (!currentUser.Tasks.Any())
            {
                ConsoleHelper.DisplayInfo("No tasks available.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== ALL TASKS ===\n");

            // Sort tasks: incomplete first, then by priority (High to Low), then by due date
            var sortedTasks = currentUser.Tasks
                .OrderBy(t => t.IsComplete) // false (0) comes before true (1)
                .ThenByDescending(t => t.Priority) // High (2), Medium (1), Low (0)
                .ThenBy(t => t.DueDate ?? DateTime.MaxValue); // Null dates last

            int taskCount = 1;
            foreach (var task in sortedTasks)
            {
                // Color coding based on priority and completion
                if (task.IsComplete)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (task.Priority == Priority.High)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (task.Priority == Priority.Medium)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"{taskCount}. {task}");
                taskCount++;
            }

            Console.ResetColor();

            // Display statistics
            int totalTasks = currentUser.Tasks.Count;
            int completedTasks = currentUser.Tasks.Count(t => t.IsComplete);
            int pendingTasks = totalTasks - completedTasks;

            Console.WriteLine("\n===================================");
            Console.WriteLine($"STATISTICS");
            Console.WriteLine("===================================");
            Console.WriteLine($"Total tasks: {totalTasks}");
            Console.WriteLine($"Completed: {completedTasks}");
            Console.WriteLine($"Pending: {pendingTasks}");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lists tasks in a simple format without colors or statistics
        /// </summary>
        private void ListTasksSimple()
        {
            if (!currentUser.Tasks.Any())
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("Your tasks:");
            foreach (var task in currentUser.Tasks.OrderBy(t => t.Id))
            {
                Console.WriteLine($"  {task}");
            }
        }

        /// <summary>
        /// Edits an existing task
        /// </summary>
        private void EditTask()
        {
            if (!currentUser.Tasks.Any())
            {
                ConsoleHelper.DisplayInfo("No tasks available to edit.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("EDIT TASK");
            Console.WriteLine("===================================\n");
            ListTasksSimple();

            int taskId = ConsoleHelper.GetIntInput("\nEnter task ID to edit: ", 1);

            TaskItem taskToEdit = currentUser.Tasks.Find(t => t.Id == taskId);

            if (taskToEdit == null)
            {
                ConsoleHelper.DisplayError($"Task with ID {taskId} not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nEditing Task ID {taskId}: {taskToEdit.Title}");
            Console.WriteLine("Leave fields blank to keep current values.\n");

            // Edit title
            string newTitle = ConsoleHelper.GetStringInput($"Title [{taskToEdit.Title}]: ", true);
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                taskToEdit.Title = newTitle;
            }

            // Edit due date
            Console.WriteLine($"Current due date: {(taskToEdit.DueDate.HasValue ? taskToEdit.DueDate.Value.ToString("yyyy-MM-dd") : "None")}");
            DateTime? newDueDate = ConsoleHelper.GetDateInput("New due date (yyyy-MM-dd) or press Enter to keep/set to none: ");
            taskToEdit.DueDate = newDueDate;

            // Edit priority
            Console.WriteLine($"Current priority: {taskToEdit.Priority}");
            Console.WriteLine("Select new priority:");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Low");
            Console.Write("Choice (1-3, press Enter to keep current): ");
            string priorityChoice = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(priorityChoice))
            {
                switch (priorityChoice)
                {
                    case "1":
                        taskToEdit.Priority = Priority.High;
                        break;
                    case "2":
                        taskToEdit.Priority = Priority.Medium;
                        break;
                    case "3":
                        taskToEdit.Priority = Priority.Low;
                        break;
                }
            }

            // Save changes
            jsonDataManager.SaveData(appData);

            ConsoleHelper.DisplaySuccess($"Task updated successfully! (ID: {taskId})");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Searches tasks by title
        /// </summary>
        private void SearchTasks()
        {
            if (!currentUser.Tasks.Any())
            {
                ConsoleHelper.DisplayInfo("No tasks available to search.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("SEARCH TASKS");
            Console.WriteLine("===================================\n");

            string searchTerm = ConsoleHelper.GetStringInput("Enter search term: ", true);

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ListTasks();
                return;
            }

            var searchResults = currentUser.Tasks
                .Where(t => t.Title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(t => t.IsComplete)
                .ThenByDescending(t => t.Priority)
                .ThenBy(t => t.DueDate ?? DateTime.MaxValue)
                .ToList();

            if (!searchResults.Any())
            {
                ConsoleHelper.DisplayInfo($"No tasks found containing '{searchTerm}'.");
            }
            else
            {
                Console.WriteLine($"\nFound {searchResults.Count} task(s) containing '{searchTerm}':\n");

                int resultCount = 1;
                foreach (var task in searchResults)
                {
                    Console.WriteLine($"{resultCount}. {task}");
                    resultCount++;
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Exits the application with confirmation
        /// </summary>
        private void ExitApplication()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit? (y/n): ");
            string response = Console.ReadLine()?.Trim().ToLower();

            if (response == "y" || response == "yes")
            {
                Console.WriteLine("\nThank you for using Daily Task Manager. Goodbye!");
                Environment.Exit(0);
            }
        }
    }
}