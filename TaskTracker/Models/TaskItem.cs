// File: Models/TaskItem.cs
using System.Text.Json.Serialization;

namespace TaskTracker.Models
{
    public class TaskItem
    {
        public enum enStatus { Todo = 0, In_Progress = 1, Done = 2, All = 3 };

        public int ID { get; set; }
        public string Description { get; set; } = string.Empty; 
        public enStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}