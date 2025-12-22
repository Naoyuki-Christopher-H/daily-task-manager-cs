using System.Collections.Generic;

namespace daily_task_manager_cs.Models
{
    /// <summary>
    /// Represents a user account with authentication and task management capabilities.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique username for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Securely hashed password for authentication
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// List of tasks associated with this user
        /// </summary>
        public List<TaskItem> Tasks { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
        {
            Tasks = new List<TaskItem>();
        }

        /// <summary>
        /// Parameterized constructor for creating a new user
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="passwordHash">Hashed password</param>
        public User(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
            Tasks = new List<TaskItem>();
        }
    }
}