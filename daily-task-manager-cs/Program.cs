using System;
using daily_task_manager_cs.Managers;
using daily_task_manager_cs.Models;
using daily_task_manager_cs.Utils;

namespace daily_task_manager_cs
{
    /// <summary>
    /// Main entry point for the Daily Task Manager application.
    /// Coordinates all components and manages the application lifecycle.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main application data container
        /// </summary>
        private static AppData appData;

        /// <summary>
        /// JSON data manager for persistence
        /// </summary>
        private static JsonDataManager jsonDataManager;

        /// <summary>
        /// Authentication manager for user operations
        /// </summary>
        private static AuthenticationManager authManager;

        /// <summary>
        /// Flag to track if welcome art has been shown
        /// </summary>
        private static bool welcomeArtShown = false;

        /// <summary>
        /// Main entry point of the application
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            try
            {
                InitializeApplication();
                RunApplication();
            }
            catch (Exception ex)
            {
                HandleFatalError(ex);
            }
        }

        /// <summary>
        /// Initializes application components and loads data
        /// </summary>
        private static void InitializeApplication()
        {
            // Initialize managers
            jsonDataManager = new JsonDataManager();
            appData = jsonDataManager.LoadData();
            authManager = new AuthenticationManager(appData, jsonDataManager);

            // Show welcome art on first run if no users exist
            if (!authManager.HasUsers() && !welcomeArtShown)
            {
                ConsoleHelper.DisplayWelcomeArt();
                welcomeArtShown = true;
            }
        }

        /// <summary>
        /// Main application loop
        /// </summary>
        private static void RunApplication()
        {
            bool running = true;

            while (running)
            {
                try
                {
                    // Show authentication menu
                    bool authSuccess = authManager.ShowAuthMenu();

                    if (!authSuccess && authManager.CurrentUser == null)
                    {
                        // User chose to exit from auth menu
                        running = false;
                        continue;
                    }

                    if (authManager.CurrentUser != null)
                    {
                        // User authenticated successfully, show task manager
                        TaskManager taskManager = new TaskManager(
                            authManager.CurrentUser,
                            jsonDataManager,
                            appData
                        );

                        taskManager.ShowTaskMenu();

                        // User logged out, clear current user
                        authManager.Logout();
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.DisplayError($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\nApplication terminated. Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles fatal application errors
        /// </summary>
        /// <param name="ex">Exception that caused the fatal error</param>
        private static void HandleFatalError(Exception ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=== FATAL ERROR ===");
            Console.WriteLine($"A fatal error occurred and the application must close.");
            Console.WriteLine($"Error details: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}