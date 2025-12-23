using daily_task_manager_cs.Models;
using daily_task_manager_cs.Utils;
using System;
using System.Collections.Generic;
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

                // Parse JSON data
                return ParseJsonData(jsonData);
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Failed to load data: {ex.Message}");
                Console.WriteLine("Creating new data file...");
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
                // Serialize to JSON
                string jsonData = SerializeToJson(data);

                // Write to file with UTF-8 encoding
                File.WriteAllText(DataFilePath, jsonData, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Failed to save data: {ex.Message}");
            }
        }

        /// <summary>
        /// Parses JSON data into AppData object
        /// </summary>
        /// <param name="jsonData">JSON string to parse</param>
        /// <returns>Parsed AppData object</returns>
        private AppData ParseJsonData(string jsonData)
        {
            AppData data = new AppData();

            // Check if JSON is empty or invalid
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return data;
            }

            try
            {
                // Simple JSON parsing for C# 4.7.2 without external libraries
                // This implementation handles the specific structure we need
                if (!jsonData.Contains("\"users\""))
                {
                    return data;
                }

                int usersStart = jsonData.IndexOf("\"users\":[") + 9;
                int usersEnd = jsonData.LastIndexOf("]");

                if (usersStart >= 9 && usersEnd > usersStart)
                {
                    string usersArray = jsonData.Substring(usersStart, usersEnd - usersStart + 1);
                    ParseUsers(data, usersArray);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Error parsing JSON data: {ex.Message}");
                return new AppData();
            }

            return data;
        }

        /// <summary>
        /// Parses users from JSON array
        /// </summary>
        /// <param name="data">AppData object to populate</param>
        /// <param name="usersArray">JSON array string of users</param>
        private void ParseUsers(AppData data, string usersArray)
        {
            int currentPos = 0;

            while (currentPos < usersArray.Length)
            {
                // Find start of user object
                int userStart = usersArray.IndexOf('{', currentPos);
                if (userStart == -1) break;

                int userEnd = usersArray.IndexOf('}', userStart);
                if (userEnd == -1) break;

                string userJson = usersArray.Substring(userStart, userEnd - userStart + 1);
                User user = ParseUser(userJson);

                if (user != null)
                {
                    data.Users.Add(user);
                }

                currentPos = userEnd + 1;
            }
        }

        /// <summary>
        /// Parses a single user from JSON
        /// </summary>
        /// <param name="userJson">JSON string of a single user</param>
        /// <returns>Parsed User object</returns>
        private User ParseUser(string userJson)
        {
            try
            {
                string username = ExtractJsonValue(userJson, "username");
                string passwordHash = ExtractJsonValue(userJson, "passwordHash");

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passwordHash))
                {
                    return null;
                }

                User user = new User(username, passwordHash);

                // Parse tasks
                int tasksStart = userJson.IndexOf("\"tasks\":[");
                if (tasksStart != -1)
                {
                    tasksStart += 9;
                    int tasksEnd = userJson.LastIndexOf("]");

                    if (tasksEnd > tasksStart)
                    {
                        string tasksArray = userJson.Substring(tasksStart, tasksEnd - tasksStart + 1);
                        ParseTasks(user, tasksArray);
                    }
                }

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Parses tasks from JSON array
        /// </summary>
        /// <param name="user">User object to add tasks to</param>
        /// <param name="tasksArray">JSON array string of tasks</param>
        private void ParseTasks(User user, string tasksArray)
        {
            int currentPos = 0;

            while (currentPos < tasksArray.Length)
            {
                // Find start of task object
                int taskStart = tasksArray.IndexOf('{', currentPos);
                if (taskStart == -1) break;

                int taskEnd = tasksArray.IndexOf('}', taskStart);
                if (taskEnd == -1) break;

                string taskJson = tasksArray.Substring(taskStart, taskEnd - taskStart + 1);
                TaskItem task = ParseTask(taskJson);

                if (task != null)
                {
                    user.Tasks.Add(task);
                }

                currentPos = taskEnd + 1;
            }
        }

        /// <summary>
        /// Parses a single task from JSON
        /// </summary>
        /// <param name="taskJson">JSON string of a single task</param>
        /// <returns>Parsed TaskItem object</returns>
        private TaskItem ParseTask(string taskJson)
        {
            try
            {
                string idStr = ExtractJsonValue(taskJson, "id");
                string title = ExtractJsonValue(taskJson, "title");
                string dueDateStr = ExtractJsonValue(taskJson, "dueDate");
                string priorityStr = ExtractJsonValue(taskJson, "priority");
                string isCompleteStr = ExtractJsonValue(taskJson, "isComplete");

                if (!int.TryParse(idStr, out int id))
                {
                    return null;
                }

                TaskItem task = new TaskItem();
                task.Id = id;
                task.Title = title ?? "";

                // Parse due date
                if (dueDateStr != "null" && !string.IsNullOrEmpty(dueDateStr))
                {
                    if (DateTime.TryParse(dueDateStr.Trim('"'), out DateTime dueDate))
                    {
                        task.DueDate = dueDate;
                    }
                }

                // Parse priority
                if (!string.IsNullOrEmpty(priorityStr))
                {
                    priorityStr = priorityStr.Trim('"');
                    if (Enum.TryParse<Priority>(priorityStr, true, out Priority priority))
                    {
                        task.Priority = priority;
                    }
                }

                // Parse completion status
                if (!string.IsNullOrEmpty(isCompleteStr))
                {
                    task.IsComplete = isCompleteStr.ToLower() == "true";
                }

                return task;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts a value from JSON string by key
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <param name="key">Key to extract value for</param>
        /// <returns>Extracted value or empty string if not found</returns>
        private string ExtractJsonValue(string json, string key)
        {
            string searchKey = $"\"{key}\":";
            int keyPos = json.IndexOf(searchKey);

            if (keyPos == -1) return "";

            int valueStart = keyPos + searchKey.Length;
            int valueEnd = json.IndexOf(',', valueStart);

            if (valueEnd == -1)
            {
                valueEnd = json.IndexOf('}', valueStart);
            }

            if (valueEnd == -1) return "";

            string value = json.Substring(valueStart, valueEnd - valueStart).Trim();

            // Remove quotes if present
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                value = value.Substring(1, value.Length - 2);
            }

            return value;
        }

        /// <summary>
        /// Serializes AppData object to JSON string
        /// </summary>
        /// <param name="data">AppData object to serialize</param>
        /// <returns>JSON string</returns>
        private string SerializeToJson(AppData data)
        {
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