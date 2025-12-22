using System;
using System.Globalization;

namespace daily_task_manager_cs.Utils
{
    /// <summary>
    /// Helper class for console input/output operations.
    /// Provides validation and formatting utilities.
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Displays ASCII art welcome message
        /// </summary>
        public static void DisplayWelcomeArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
   ____        _     _____         _     __  __
  |  _ \  __ _| |_  |_   _|__  ___| |_  |  \/  | __ _ _ __   __ _  __ _  ___ _ __
  | | | |/ _` | __|   | |/ _ \/ __| __| | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__|
  | |_| | (_| | |_    | |  __/\__ \ |_  | |  | | (_| | | | | (_| | (_| |  __/ |
  |____/ \__,_|\__|   |_|\___||___/\__| |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|
                                                                  |___/");
            Console.WriteLine("\nWelcome to Daily Task Manager!\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Prompts user for a string with validation
        /// </summary>
        /// <param name="prompt">Prompt message to display</param>
        /// <param name="allowEmpty">Whether empty input is allowed</param>
        /// <returns>Validated string input</returns>
        public static string GetStringInput(string prompt, bool allowEmpty = false)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();

                if (!allowEmpty && string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input cannot be empty. Please try again.");
                    Console.ResetColor();
                }
            }
            while (!allowEmpty && string.IsNullOrWhiteSpace(input));

            return input;
        }

        /// <summary>
        /// Prompts user for a date input with validation
        /// </summary>
        /// <param name="prompt">Prompt message to display</param>
        /// <param name="allowEmpty">Whether empty input is allowed</param>
        /// <returns>Nullable DateTime if valid input, null if empty and allowed</returns>
        public static DateTime? GetDateInput(string prompt, bool allowEmpty = true)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (allowEmpty)
                    {
                        return null;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Date is required. Please try again.");
                        Console.ResetColor();
                        continue;
                    }
                }

                // Try to parse the date in yyyy-MM-dd format
                if (DateTime.TryParseExact(input, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please use YYYY-MM-DD format (e.g., 2024-12-31).");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Prompts user for a priority level with validation
        /// </summary>
        /// <returns>Selected Priority enum value</returns>
        public static Models.Priority GetPriorityInput()
        {
            while (true)
            {
                Console.WriteLine("\nSelect priority level:");
                Console.WriteLine("1. High");
                Console.WriteLine("2. Medium (default)");
                Console.WriteLine("3. Low");
                Console.Write("Enter choice (1-3): ");

                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return Models.Priority.Medium;
                }

                switch (input)
                {
                    case "1":
                        return Models.Priority.High;
                    case "2":
                        return Models.Priority.Medium;
                    case "3":
                        return Models.Priority.Low;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please enter 1, 2, or 3.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        /// <summary>
        /// Prompts user for an integer with validation
        /// </summary>
        /// <param name="prompt">Prompt message to display</param>
        /// <param name="minValue">Minimum allowed value</param>
        /// <param name="maxValue">Maximum allowed value</param>
        /// <returns>Validated integer input</returns>
        public static int GetIntInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int result))
                {
                    if (result >= minValue && result <= maxValue)
                    {
                        return result;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Input must be between {minValue} and {maxValue}. Please try again.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Displays a success message with green color
        /// </summary>
        /// <param name="message">Success message to display</param>
        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an error message with red color
        /// </summary>
        /// <param name="message">Error message to display</param>
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays an info message with yellow color
        /// </summary>
        /// <param name="message">Info message to display</param>
        public static void DisplayInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}