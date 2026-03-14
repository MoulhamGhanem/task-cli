using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskTracker
{
    public class clsTask
    {
        const string FilePath = @"D:\Tasks.JSON";

        private static int counter = 0;
        public enum enStatus { Todo=0,In_Progress=1 , Done = 2 , All=3};
        public enum enMode { AddNew=0, Update=1 };

        public int ID { get; set; } 
        public string? Description { get; set; }
        public enStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        [JsonIgnore]
        public enMode Mode {  get; set; }

        private int GetNewID()
        {
            if (File.Exists(FilePath))
            {
                int NewID = 0;
                string json = File.ReadAllText(FilePath);
                List<clsTask> Tasks = JsonSerializer.Deserialize<List<clsTask>>(json);
                if (Tasks != null)
                {
                    foreach (var task in Tasks)
                    {
                        if (task.ID > NewID)
                            NewID = task.ID;
                    }
                }
                return NewID + 1;
            }
            else
                return 1;

        }

        // 1. الباني الافتراضي: مخصص فقط لمكتبة الـ JSON لتقرأ الملفات القديمة
        public clsTask()
        {
            // الـ JSON Serializer رح يتكفل بتعبئة باقي الخصائص (ID, Status, Dates) من الملف
            Description = "";
            Mode = enMode.Update; // إذا قرأناه من الملف فهو أكيد Update وليس AddNew
        }

        public clsTask(string Description)
        {
            this.Description = Description;
            ID = GetNewID();
            Mode = enMode.AddNew;
            Status = enStatus.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

        }

        public static bool list(enStatus status = clsTask.enStatus.All)
        {
            var Tasks = GetAllTasks();

            // تأكدنا إن اللائحة مو فاضية وموجودة
            if (Tasks == null || Tasks.Count == 0)
            {
                Console.WriteLine("No Tasks Found.");
                return false;
            }

            // حلقة وحدة بتلف على كل المهام
            foreach (var Task in Tasks)
            {
                // الشرط الذكي: اطبع إذا كنا طالبين "الكل" أو إذا الحالة مطابقة
                if (status == enStatus.All || Task.Status == status)
                {
                    Console.WriteLine($"ID : {Task.ID}, Description : {Task.Description} , Status : {Task.Status} , CreatedAt : {Task.CreatedAt} , UpdatedAt : {Task.UpdatedAt}");
                }
            }

            return true;
        }

        private static void WriteJsonToFile(clsTask task, string FilePath = FilePath)
        {
            string json = "";
            if (File.Exists(FilePath))
            {
                string existingJson = File.ReadAllText(FilePath);
                var allTasks = JsonSerializer.Deserialize<List<clsTask>>(existingJson);

                allTasks!.Add(task);
                json = JsonSerializer.Serialize(allTasks);
                File.WriteAllText(FilePath, json);
            }
            else
            {
                List<clsTask> tasks = new List<clsTask>();
                tasks.Add(task);
                json = JsonSerializer.Serialize(tasks);
                File.WriteAllText(FilePath, json);
            }
        }
       
        private void _AddNewTask()
        {
            WriteJsonToFile(this);
        }
        public clsTask GetItem(List<clsTask> Tasks, int ID)
        {
            foreach (var task in Tasks)
            {
                if (task.ID == ID)
                    return task;
            }
            return null;

        }

        private static List<clsTask> GetAllTasks()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                List<clsTask> Tasks = JsonSerializer.Deserialize<List<clsTask>>(json);
                return Tasks;
            }
            return null;
        }

        public void SaveAllTasks(List<clsTask> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(FilePath, json);
        }
        private  bool _UpdateTask()
        {
            List<clsTask> Tasks = GetAllTasks();
            clsTask Task = GetItem(Tasks, this.ID);
            if (Task != null)
            {
                Task.Description = this.Description;
                Task.UpdatedAt = DateTime.Now;
                SaveAllTasks(Tasks);
                return true;
            }
            return false;
        }

        public bool DeleteTask()
        {
            List<clsTask> Tasks = GetAllTasks();
            clsTask Task = GetItem(Tasks, this.ID);
            if (Task != null)
            {
                Tasks.Remove(Task);
                SaveAllTasks(Tasks);
                return true;
            }
            return false;
        }

        public bool ChangeStatus()
        {
            List<clsTask> Tasks = GetAllTasks();
            clsTask Task = GetItem(Tasks, this.ID);
            if (Task != null)
            {
                Task.Status = this.Status;
                Task.UpdatedAt = DateTime.Now;
                SaveAllTasks(Tasks);
                return true;
            }
            return false;
        }

       



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    _AddNewTask();
                    Mode = enMode.Update;
                    return true;

                case enMode.Update:
                    return _UpdateTask();
                    

                default:
                    return false;
            }
        }
    }
}
