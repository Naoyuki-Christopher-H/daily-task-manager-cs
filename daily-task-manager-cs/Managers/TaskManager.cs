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
        /// Currently logged in user.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// JSON data manager for persistence.
        /// </summary>
        private JsonDataManager jsonDataManager;

        /// <summary>
        /// Application data reference.
        /// </summary>
        private AppData appData;

        /// <summary>
        /// Constructor for TaskManager.
        /// </summary>
        /// <param name="user">Current user object.</param>
        /// <param name="dataManager">JSON data manager instance.</param>
        /// <param name="data">Application data container.</param>
        public TaskManager(User user, JsonDataManager dataManager, AppData data)
        {
            currentUser = user;
            jsonDataManager = dataManager;
            appData = data;
        }

        /// <summary>
        /// Displays the main task management menu and handles user input.
        /// </summary>
        public void ShowTaskMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===================================");
                Console.WriteLine("DAILY TASK MANAGER");
                Console.WriteLine("Welcome, " + currentUser.Username + "!");
                Console.WriteLine("Today's Date: " + DateTime.Today.ToString("yyyy-MM-dd"));
                Console.WriteLine("===================================");
                Console.WriteLine("1. Add a new task");
                Console.WriteLine("2. Remove a task");
                Console.WriteLine("3. Mark a task as complete");
                Console.WriteLine("4. List all tasks");
                Console.WriteLine("5. Edit a task");
                Console.WriteLine("6. Search tasks");
                Console.WriteLine("7. View overdue tasks");
                Console.WriteLine("8. View today's tasks");
                Console.WriteLine("9. Logout");
                Console.WriteLine("10. Exit application");
                Console.Write("\nEnter your choice (1-10): ");

                string choice = Console.ReadLine();
                if (choice == null)
                {
                    choice = string.Empty;
                }
                choice = choice.Trim();

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
                        ShowOverdueTasks();
                        break;
                    case "8":
                        ShowTodaysTasks();
                        break;
                    case "9":
                        return; // Logout
                    case "10":
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
        /// Adds a new task for the current user.
        /// </summary>
        private void AddTask()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("ADD NEW TASK");
            Console.WriteLine("Today's Date: " + DateTime.Today.ToString("yyyy-MM-dd"));
            Console.WriteLine("===================================\n");

            // Get task title
            string title = ConsoleHelper.GetStringInput("Enter task title: ");

            // Get due date (optional)
            DateTime? dueDate = ConsoleHelper.GetDateInput("Enter due date (YYYY-MM-DD) OR press Enter to skip: ");

            // Validate due date is not in the past
            if (dueDate.HasValue)
            {
                if (dueDate.Value.Date < DateTime.Today)
                {
                    ConsoleHelper.DisplayError("Due date cannot be in the past. Please enter today's date or a future date.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
            }

            // Get priority
            Priority priority = ConsoleHelper.GetPriorityInput();

            // Generate new task ID
            int newId = 1;
            if (currentUser.Tasks.Count > 0)
            {
                newId = currentUser.Tasks.Max(t => t.Id) + 1;
            }

            // Create new task
            TaskItem newTask = new TaskItem(newId, title, dueDate, priority);

            // Add to user's tasks
            currentUser.Tasks.Add(newTask);

            // Save to JSON
            jsonDataManager.SaveData(appData);

            ConsoleHelper.DisplaySuccess("Task added successfully! (ID: " + newId + ")");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Removes a task by ID.
        /// </summary>
        private void RemoveTask()
        {
            if (currentUser.Tasks.Count == 0)
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
                ConsoleHelper.DisplayError("Task with ID " + taskId + " not found.");
            }
            else
            {
                currentUser.Tasks.Remove(taskToRemove);
                jsonDataManager.SaveData(appData);
                ConsoleHelper.DisplaySuccess("Task removed successfully! (ID: " + taskId + ")");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Marks a task as complete by ID.
        /// </summary>
        private void MarkTaskComplete()
        {
            if (currentUser.Tasks.Count == 0)
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
                ConsoleHelper.DisplayError("Task with ID " + taskId + " not found.");
            }
            else if (taskToComplete.IsComplete)
            {
                ConsoleHelper.DisplayInfo("Task with ID " + taskId + " is already complete.");
            }
            else
            {
                taskToComplete.IsComplete = true;
                jsonDataManager.SaveData(appData);
                ConsoleHelper.DisplaySuccess("Task marked as complete! (ID: " + taskId + ")");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lists all tasks with sorting and filtering options.
        /// </summary>
        private void ListTasks()
        {
            if (currentUser.Tasks.Count == 0)
            {
                ConsoleHelper.DisplayInfo("No tasks available.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("ALL TASKS");
            Console.WriteLine("Today's Date: " + DateTime.Today.ToString("yyyy-MM-dd"));
            Console.WriteLine("===================================\n");

            // Sort tasks: incomplete first, then by due date (closest first), then by priority
            var sortedTasks = currentUser.Tasks
                .OrderBy(t => t.IsComplete) // false (0) comes before true (1)
                .ThenBy(t => t.DueDate ?? DateTime.MaxValue) // Null dates last
                .ThenByDescending(t => t.Priority) // High (2), Medium (1), Low (0)
                .ToList();

            int taskCount = 1;
            foreach (var task in sortedTasks)
            {
                // Color coding based on status
                if (task.IsComplete)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (task.DueDate.HasValue && task.DueDate.Value.Date < DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Overdue
                }
                else if (task.DueDate.HasValue && task.DueDate.Value.Date == DateTime.Today)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Due today
                }
                else if (task.Priority == Priority.High)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta; // High priority
                }
                else if (task.Priority == Priority.Medium)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan; // Medium priority
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Low priority
                }

                // Add indicator for today's tasks
                string todayIndicator = "";
                if (task.DueDate.HasValue && task.DueDate.Value.Date == DateTime.Today)
                {
                    todayIndicator = " [TODAY]";
                }

                Console.WriteLine(taskCount + ". " + task.ToString() + todayIndicator);
                taskCount++;
            }

            Console.ResetColor();

            // Display statistics
            int totalTasks = currentUser.Tasks.Count;
            int completedTasks = currentUser.Tasks.Count(t => t.IsComplete);
            int pendingTasks = totalTasks - completedTasks;
            int overdueTasks = currentUser.Tasks.Count(t => !t.IsComplete &&
                                                          t.DueDate.HasValue &&
                                                          t.DueDate.Value.Date < DateTime.Today);
            int todayTasks = currentUser.Tasks.Count(t => !t.IsComplete &&
                                                         t.DueDate.HasValue &&
                                                         t.DueDate.Value.Date == DateTime.Today);

            Console.WriteLine("\n===================================");
            Console.WriteLine("STATISTICS");
            Console.WriteLine("===================================");
            Console.WriteLine("Total tasks: " + totalTasks);
            Console.WriteLine("Completed: " + completedTasks);
            Console.WriteLine("Pending: " + pendingTasks);

            if (overdueTasks > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Overdue: " + overdueTasks);
                Console.ResetColor();
            }

            if (todayTasks > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Due today: " + todayTasks);
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Lists tasks in a simple format without colors or statistics.
        /// </summary>
        private void ListTasksSimple()
        {
            if (currentUser.Tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("Your tasks:");
            foreach (var task in currentUser.Tasks.OrderBy(t => t.Id))
            {
                Console.WriteLine("  " + task.ToString());
            }
        }

        /// <summary>
        /// Edits an existing task.
        /// </summary>
        private void EditTask()
        {
            if (currentUser.Tasks.Count == 0)
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
                ConsoleHelper.DisplayError("Task with ID " + taskId + " not found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nEditing Task ID " + taskId + ": " + taskToEdit.Title);
            Console.WriteLine("Leave fields blank to keep current values.\n");

            // Edit title
            string newTitle = ConsoleHelper.GetStringInput("Title [" + taskToEdit.Title + "]: ", true);
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                taskToEdit.Title = newTitle;
            }

            // Edit due date
            Console.WriteLine("Current due date: " + (taskToEdit.DueDate.HasValue ? taskToEdit.DueDate.Value.ToString("yyyy-MM-dd") : "None"));
            DateTime? newDueDate = ConsoleHelper.GetDateInput("New due date (yyyy-MM-dd) or press Enter to keep/set to none: ", true);

            // Validate new due date if provided
            if (newDueDate.HasValue && newDueDate.Value.Date < DateTime.Today)
            {
                ConsoleHelper.DisplayError("Due date cannot be in the past. Keeping current due date.");
            }
            else if (newDueDate.HasValue || string.IsNullOrEmpty(newDueDate.ToString()))
            {
                // Only update if a new value was provided (even if empty)
                taskToEdit.DueDate = newDueDate;
            }

            // Edit priority
            Console.WriteLine("Current priority: " + taskToEdit.Priority);
            Console.WriteLine("Select new priority:");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Low");
            Console.Write("Choice (1-3, press Enter to keep current): ");
            string priorityChoice = Console.ReadLine();
            if (priorityChoice == null)
            {
                priorityChoice = string.Empty;
            }
            priorityChoice = priorityChoice.Trim();

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

            ConsoleHelper.DisplaySuccess("Task updated successfully! (ID: " + taskId + ")");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Searches tasks by title.
        /// </summary>
        private void SearchTasks()
        {
            if (currentUser.Tasks.Count == 0)
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

            if (searchResults.Count == 0)
            {
                ConsoleHelper.DisplayInfo("No tasks found containing '" + searchTerm + "'.");
            }
            else
            {
                Console.WriteLine("\nFound " + searchResults.Count + " task(s) containing '" + searchTerm + "':\n");

                int resultCount = 1;
                foreach (var task in searchResults)
                {
                    // Color coding for search results
                    if (task.IsComplete)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else if (task.DueDate.HasValue && task.DueDate.Value.Date < DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (task.DueDate.HasValue && task.DueDate.Value.Date == DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (task.Priority == Priority.High)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }

                    Console.WriteLine(resultCount + ". " + task.ToString());
                    Console.ResetColor();
                    resultCount++;
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows tasks that are overdue (due date before today and not completed).
        /// </summary>
        private void ShowOverdueTasks()
        {
            var overdueTasks = currentUser.Tasks
                .Where(t => !t.IsComplete &&
                           t.DueDate.HasValue &&
                           t.DueDate.Value.Date < DateTime.Today)
                .OrderBy(t => t.DueDate)
                .ToList();

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("OVERDUE TASKS");
            Console.WriteLine("Today's Date: " + DateTime.Today.ToString("yyyy-MM-dd"));
            Console.WriteLine("===================================\n");

            if (overdueTasks.Count == 0)
            {
                ConsoleHelper.DisplaySuccess("No overdue tasks! You are all caught up.");
            }
            else
            {
                Console.WriteLine("You have " + overdueTasks.Count + " overdue task(s):\n");

                int taskCount = 1;
                foreach (var task in overdueTasks)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    int daysOverdue = (DateTime.Today - task.DueDate.Value.Date).Days;
                    Console.WriteLine(taskCount + ". " + task.ToString() + " (Overdue by " + daysOverdue + " day(s))");
                    taskCount++;
                }
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Shows tasks that are due today (due date equals today's date and not completed).
        /// </summary>
        private void ShowTodaysTasks()
        {
            var todaysTasks = currentUser.Tasks
                .Where(t => !t.IsComplete &&
                           t.DueDate.HasValue &&
                           t.DueDate.Value.Date == DateTime.Today)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.Title)
                .ToList();

            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("TODAY'S TASKS");
            Console.WriteLine("Today's Date: " + DateTime.Today.ToString("yyyy-MM-dd"));
            Console.WriteLine("===================================\n");

            if (todaysTasks.Count == 0)
            {
                ConsoleHelper.DisplaySuccess("No tasks due today! You are all caught up.");
            }
            else
            {
                Console.WriteLine("You have " + todaysTasks.Count + " task(s) due today:\n");

                int taskCount = 1;
                foreach (var task in todaysTasks)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(taskCount + ". " + task.ToString());
                    taskCount++;
                }
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Exits the application with confirmation.
        /// </summary>
        private void ExitApplication()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit? (Y/N): ");
            string response = Console.ReadLine();
            if (response == null)
            {
                response = string.Empty;
            }
            response = response.Trim().ToLower();

            if (response == "y" || response == "yes")
            {
                Console.WriteLine("\nThank you for using Daily Task Manager. Goodbye!");
                Environment.Exit(0);
            }
        }
    }
}