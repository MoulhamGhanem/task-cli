using System;

namespace TaskTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. التأكد من وجود أمر
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: task-cli <command> [arguments]");
                return;
            }

            // 2. قراءة الأمر الأساسي وتحويله لأحرف صغيرة
            string command = args[0].ToLower();

            // 3. توجيه الأوامر
            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Please provide a task description.");
                        return;
                    }
                    clsTask newTask = new clsTask(args[1]);
                    if (newTask.Save())
                        Console.WriteLine($"Task added successfully (ID: {newTask.ID})");
                    else
                        Console.WriteLine("Failed to add task.");
                    break;

                case "update":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("Error: Please provide a task ID and a new description.");
                        return;
                    }
                    if (int.TryParse(args[1], out int updateId))
                    {
                        clsTask updateTask = new clsTask();
                        updateTask.ID = updateId;
                        updateTask.Description = args[2];
                        if (updateTask.Save())
                            Console.WriteLine($"Task {updateId} updated successfully.");
                        else
                            Console.WriteLine($"Task {updateId} not found.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid ID format.");
                    }
                    break;

                case "delete":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Please provide a task ID.");
                        return;
                    }
                    if (int.TryParse(args[1], out int deleteId))
                    {
                        clsTask deleteTask = new clsTask();
                        deleteTask.ID = deleteId;
                        if (deleteTask.DeleteTask())
                            Console.WriteLine($"Task {deleteId} deleted successfully.");
                        else
                            Console.WriteLine($"Task {deleteId} not found.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid ID format.");
                    }
                    break;

                case "mark-in-progress":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Please provide a task ID.");
                        return;
                    }
                    if (int.TryParse(args[1], out int progId))
                    {
                        clsTask progTask = new clsTask();
                        progTask.ID = progId;
                        progTask.Status = clsTask.enStatus.In_Progress;
                        if (progTask.ChangeStatus())
                            Console.WriteLine($"Task {progId} marked as in-progress.");
                        else
                            Console.WriteLine($"Task {progId} not found.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid ID format.");
                    }
                    break;

                case "mark-done":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Please provide a task ID.");
                        return;
                    }
                    if (int.TryParse(args[1], out int doneId))
                    {
                        clsTask doneTask = new clsTask();
                        doneTask.ID = doneId;
                        doneTask.Status = clsTask.enStatus.Done;
                        if (doneTask.ChangeStatus())
                            Console.WriteLine($"Task {doneId} marked as done.");
                        else
                            Console.WriteLine($"Task {doneId} not found.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid ID format.");
                    }
                    break;

                case "list":
                    if (args.Length == 1)
                    {
                        clsTask.list(); // يعرض كل المهام
                    }
                    else
                    {
                        string filter = args[1].ToLower();
                        if (filter == "done")
                            clsTask.list(clsTask.enStatus.Done);
                        else if (filter == "todo")
                            clsTask.list(clsTask.enStatus.Todo);
                        else if (filter == "in-progress")
                            clsTask.list(clsTask.enStatus.In_Progress);
                        else
                            Console.WriteLine("Error: Invalid list filter. Use done, todo, or in-progress.");
                    }
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }
}