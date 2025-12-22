using daily_task_manager_cs.Models;
using daily_task_manager_cs.Utils;
using System;
using System.IO;
using System.Text;

namespace daily_task_manager_cs.Managers
{
    /// <summary>
    /// Manages JSON data persistence for the application.
    /// Handles loading and saving of application data to/from a JSON file.
    /// </summary>
    public class JsonDataManager
    {
        /// <summary>
        /// File path for the JSON data file
        /// </summary>
        private const string DataFilePath = "tasks.json";

        /// <summary>
        /// Loads application data from JSON file
        /// </summary>
        /// <returns>AppData object containing all application data</returns>
        public AppData LoadData()
        {
            try
            {
                // Check if file exists
                if (!File.Exists(DataFilePath))
                {
                    // Create new empty data structure
                    return new AppData();
                }

                // Read JSON from file
                string jsonData = File.ReadAllText(DataFilePath, Encoding.UTF8);

                // In a full implementation, you would use Newtonsoft.Json or System.Text.Json
                // For C# 4.7.2, we'll use a simple custom parser for this example
                // Note: For production, use a proper JSON library like Newtonsoft.Json
                return ParseJsonData(jsonData);
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Failed to load data: {ex.Message}");
                return new AppData();
            }
        }

        /// <summary>
        /// Saves application data to JSON file
        /// </summary>
        /// <param name="data">AppData object to save</param>
        public void SaveData(AppData data)
        {
            try
            {
                // In a full implementation, you would serialize to JSON
                // For this example, we'll use a simple serialization approach
                string jsonData = SerializeToJson(data);

                // Write to file with UTF-8 encoding
                File.WriteAllText(DataFilePath, jsonData, Encoding.UTF8);

                ConsoleHelper.DisplaySuccess("Data saved successfully.");
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Failed to save data: {ex.Message}");
            }
        }

        /// <summary>
        /// Simple JSON parser for the application data structure
        /// </summary>
        /// <param name="jsonData">JSON string to parse</param>
        /// <returns>Parsed AppData object</returns>
        private AppData ParseJsonData(string jsonData)
        {
            // This is a simplified parser for the example
            // In a real application, use Newtonsoft.Json or System.Text.Json

            AppData data = new AppData();

            // Check if JSON is empty or invalid
            if (string.IsNullOrWhiteSpace(jsonData) || !jsonData.Contains("\"users\""))
            {
                return data;
            }

            try
            {
                // Extract users array
                int usersStart = jsonData.IndexOf("\"users\":") + 8;
                int usersEnd = jsonData.LastIndexOf("]");

                if (usersStart >= 8 && usersEnd > usersStart)
                {
                    string usersJson = jsonData.Substring(usersStart, usersEnd - usersStart + 1);

                    // Parse users (simplified - would be more complex in real implementation)
                    // For this example, we'll return empty data
                    // In production, use: JsonConvert.DeserializeObject<AppData>(jsonData)
                }
            }
            catch (Exception)
            {
                // If parsing fails, return empty data
                return new AppData();
            }

            return data;
        }

        /// <summary>
        /// Simple JSON serializer for the application data structure
        /// </summary>
        /// <param name="data">AppData object to serialize</param>
        /// <returns>JSON string</returns>
        private string SerializeToJson(AppData data)
        {
            // This is a simplified serializer for the example
            // In a real application, use Newtonsoft.Json or System.Text.Json

            StringBuilder json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("  \"users\": [");

            for (int i = 0; i < data.Users.Count; i++)
            {
                User user = data.Users[i];
                json.Append("    {");
                json.Append($"\"username\": \"{EscapeJsonString(user.Username)}\", ");
                json.Append($"\"passwordHash\": \"{EscapeJsonString(user.PasswordHash)}\", ");
                json.AppendLine("\"tasks\": [");

                for (int j = 0; j < user.Tasks.Count; j++)
                {
                    TaskItem task = user.Tasks[j];
                    json.Append("      {");
                    json.Append($"\"id\": {task.Id}, ");
                    json.Append($"\"title\": \"{EscapeJsonString(task.Title)}\", ");
                    json.Append($"\"dueDate\": {(task.DueDate.HasValue ? $"\"{task.DueDate.Value:yyyy-MM-dd}\"" : "null")}, ");
                    json.Append($"\"priority\": \"{task.Priority}\", ");
                    json.Append($"\"isComplete\": {task.IsComplete.ToString().ToLower()}");
                    json.Append("}");

                    if (j < user.Tasks.Count - 1)
                    {
                        json.Append(",");
                    }
                    json.AppendLine();
                }

                json.Append("    ]}");

                if (i < data.Users.Count - 1)
                {
                    json.Append(",");
                }
                json.AppendLine();
            }

            json.AppendLine("  ]");
            json.AppendLine("}");

            return json.ToString();
        }

        /// <summary>
        /// Escapes special characters in JSON strings
        /// </summary>
        /// <param name="str">String to escape</param>
        /// <returns>Escaped string</returns>
        private string EscapeJsonString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace("\\", "\\\\")
                     .Replace("\"", "\\\"")
                     .Replace("\n", "\\n")
                     .Replace("\r", "\\r")
                     .Replace("\t", "\\t");
        }
    }
}