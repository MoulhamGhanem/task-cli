// File: Services/TaskManager.cs
using System.Text.Json;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskManager : ITaskManager
    {
        
        private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

       
        private List<TaskItem> GetAllTasks()
        {
            if (!File.Exists(FilePath))
                return new List<TaskItem>();

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                return new List<TaskItem>(); 
            }
        }

        private void SaveAllTasks(List<TaskItem> tasks)
        {
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }); 
            File.WriteAllText(FilePath, json);
        }

        private int GetNewID(List<TaskItem> tasks)
        {
            if (tasks.Count == 0) return 1;
            int maxId = 0;
            foreach (var task in tasks)
            {
                if (task.ID > maxId) maxId = task.ID;
            }
            return maxId + 1;
        }

        private TaskItem GetItem(List<TaskItem> tasks, int id)
        {
            foreach (var task in tasks)
            {
                if (task.ID == id) return task;
            }
            return null;
        }

        //  (CRUD)
        public TaskItem AddNewTask(string description)
        {
            var tasks = GetAllTasks();
            var newTask = new TaskItem
            {
                ID = GetNewID(tasks),
                Description = description,
                Status = TaskItem.enStatus.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            tasks.Add(newTask);
            SaveAllTasks(tasks);
            return newTask; 
        }

        public bool UpdateTask(int id, string newDescription)
        {
            var tasks = GetAllTasks();
            var task = GetItem(tasks, id);

            if (task != null)
            {
                task.Description = newDescription;
                task.UpdatedAt = DateTime.Now;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            var tasks = GetAllTasks();
            var task = GetItem(tasks, id);

            if (task != null)
            {
                tasks.Remove(task);
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        public bool ChangeStatus(int id, TaskItem.enStatus newStatus)
        {
            var tasks = GetAllTasks();
            var task = GetItem(tasks, id);

            if (task != null)
            {
                task.Status = newStatus;
                task.UpdatedAt = DateTime.Now;
                SaveAllTasks(tasks);
                return true;
            }
            return false;
        }

        
        public List<TaskItem> ListTasks(TaskItem.enStatus filterStatus = TaskItem.enStatus.All)
        {
            var allTasks = GetAllTasks();
            var filteredTasks = new List<TaskItem>();

            foreach (var task in allTasks)
            {
                if (filterStatus == TaskItem.enStatus.All || task.Status == filterStatus)
                {
                    filteredTasks.Add(task);
                }
            }
            return filteredTasks;
        }
    }
}