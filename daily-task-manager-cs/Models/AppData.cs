using System.Collections.Generic;

namespace daily_task_manager_cs.Models
{
    /// <summary>
    /// Main data container for the application.
    /// Stores all users and their associated tasks.
    /// </summary>
    public class AppData
    {
        /// <summary>
        /// List of all registered users in the application
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppData()
        {
            Users = new List<User>();
        }

        /// <summary>
        /// Finds a user by username
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>User object if found, null otherwise</returns>
        public User FindUser(string username)
        {
            return Users.Find(u => u.Username == username);
        }
    }
}