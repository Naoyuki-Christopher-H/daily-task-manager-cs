using System;
using System.Linq;
using daily_task_manager_cs.Models;
using daily_task_manager_cs.Utils;

namespace daily_task_manager_cs.Managers
{
    /// <summary>
    /// Manages user authentication including login, registration, and user management.
    /// Handles secure password hashing and verification.
    /// </summary>
    public class AuthenticationManager
    {
        /// <summary>
        /// Reference to the main application data
        /// </summary>
        private AppData appData;

        /// <summary>
        /// Currently logged in user
        /// </summary>
        public User CurrentUser { get; private set; }

        /// <summary>
        /// JSON data manager for persistence
        /// </summary>
        private JsonDataManager jsonDataManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Application data reference</param>
        /// <param name="dataManager">JSON data manager</param>
        public AuthenticationManager(AppData data, JsonDataManager dataManager)
        {
            appData = data;
            jsonDataManager = dataManager;
            CurrentUser = null;
        }

        /// <summary>
        /// Displays authentication menu and handles user choice
        /// </summary>
        /// <returns>True if authentication successful, false otherwise</returns>
        public bool ShowAuthMenu()
        {
            Console.Clear();
            Console.WriteLine("=== AUTHENTICATION ===\n");
            Console.WriteLine("1. Create new account");
            Console.WriteLine("2. Login to existing account");
            Console.WriteLine("3. Exit application");
            Console.Write("\nEnter your choice (1-3): ");

            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    return RegisterUser();
                case "2":
                    return LoginUser();
                case "3":
                    Console.WriteLine("\nGoodbye!");
                    return false;
                default:
                    ConsoleHelper.DisplayError("Invalid choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return ShowAuthMenu();
            }
        }

        /// <summary>
        /// Registers a new user with username and password
        /// </summary>
        /// <returns>True if registration successful, false otherwise</returns>
        private bool RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE NEW ACCOUNT ===\n");

            // Get username
            string username = ConsoleHelper.GetStringInput("Enter username: ");

            // Check if username already exists
            if (appData.FindUser(username) != null)
            {
                ConsoleHelper.DisplayError("Username already exists. Please choose a different username.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }

            // Get password
            string password = ConsoleHelper.GetStringInput("Enter password: ");

            // Confirm password
            string confirmPassword = ConsoleHelper.GetStringInput("Confirm password: ");

            if (password != confirmPassword)
            {
                ConsoleHelper.DisplayError("Passwords do not match. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }

            // Hash the password
            string passwordHash = PasswordHasher.HashPassword(password);

            // Create new user
            User newUser = new User(username, passwordHash);

            // Add to app data
            appData.Users.Add(newUser);

            // Save to JSON file
            jsonDataManager.SaveData(appData);

            // Set as current user
            CurrentUser = newUser;

            ConsoleHelper.DisplaySuccess($"Account created successfully! Welcome, {username}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return true;
        }

        /// <summary>
        /// Logs in an existing user with username and password
        /// </summary>
        /// <returns>True if login successful, false otherwise</returns>
        private bool LoginUser()
        {
            Console.Clear();
            Console.WriteLine("=== LOGIN ===\n");

            // Get username
            string username = ConsoleHelper.GetStringInput("Username: ");

            // Get password
            string password = ConsoleHelper.GetStringInput("Password: ");

            // Find user
            User user = appData.FindUser(username);

            if (user == null)
            {
                ConsoleHelper.DisplayError("Invalid username or password.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }

            // Verify password
            if (!PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                ConsoleHelper.DisplayError("Invalid username or password.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return false;
            }

            // Login successful
            CurrentUser = user;

            ConsoleHelper.DisplaySuccess($"Login successful! Welcome back, {username}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return true;
        }

        /// <summary>
        /// Logs out the current user
        /// </summary>
        public void Logout()
        {
            CurrentUser = null;
            ConsoleHelper.DisplaySuccess("Logged out successfully.");
        }

        /// <summary>
        /// Checks if any users exist in the system
        /// </summary>
        /// <returns>True if users exist, false otherwise</returns>
        public bool HasUsers()
        {
            return appData.Users.Any();
        }
    }
}