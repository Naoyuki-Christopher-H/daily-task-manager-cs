using System;

namespace daily_task_manager_cs.Models
{
    /// <summary>
    /// Represents a task item in the task manager.
    /// Contains all properties needed for task management.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Unique identifier for the task
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title or description of the task (required)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Optional due date for the task
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Priority level of the task (default: Medium)
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Completion status of the task
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskItem()
        {
            Priority = Priority.Medium;
            IsComplete = false;
        }

        /// <summary>
        /// Parameterized constructor for creating a new task
        /// </summary>
        /// <param name="id">Task identifier</param>
        /// <param name="title">Task title</param>
        /// <param name="dueDate">Optional due date</param>
        /// <param name="priority">Task priority</param>
        public TaskItem(int id, string title, DateTime? dueDate, Priority priority)
        {
            Id = id;
            Title = title;
            DueDate = dueDate;
            Priority = priority;
            IsComplete = false;
        }

        /// <summary>
        /// Returns a string representation of the task
        /// </summary>
        /// <returns>Formatted task string</returns>
        public override string ToString()
        {
            string dueDateStr = DueDate.HasValue ? DueDate.Value.ToString("yyyy-MM-dd") : "No due date";
            string status = IsComplete ? "✓ Completed" : "✗ Pending";
            return $"ID: {Id} | {status} | Priority: {Priority} | Due: {dueDateStr} | {Title}";
        }
    }
}