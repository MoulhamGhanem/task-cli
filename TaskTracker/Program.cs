// File: Program.cs
using System;
using TaskTracker.Interfaces;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: task-cli <command> [arguments]");
                return;
            }

            string command = args[0].ToLower();
            ITaskManager taskManager = new TaskManager();

            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Please provide a task description.");
                        return;
                    }
                    var addedTask = taskManager.AddNewTask(args[1]);
                    Console.WriteLine($"Task added successfully (ID: {addedTask.ID})");
                    break;

                case "update":
                    if (args.Length < 3) { Console.WriteLine("Error: Please provide ID and description."); return; }
                    if (int.TryParse(args[1], out int updateId))
                    {
                        if (taskManager.UpdateTask(updateId, args[2]))
                            Console.WriteLine($"Task {updateId} updated successfully.");
                        else
                            Console.WriteLine($"Task {updateId} not found.");
                    }
                    else { Console.WriteLine("Error: Invalid ID format."); }
                    break;

                case "delete":
                    if (args.Length < 2) { Console.WriteLine("Error: Please provide a task ID."); return; }
                    if (int.TryParse(args[1], out int deleteId))
                    {
                        if (taskManager.DeleteTask(deleteId))
                            Console.WriteLine($"Task {deleteId} deleted successfully.");
                        else
                            Console.WriteLine($"Task {deleteId} not found.");
                    }
                    else { Console.WriteLine("Error: Invalid ID format."); }
                    break;

                case "mark-in-progress":
                    if (args.Length < 2) { Console.WriteLine("Error: Please provide a task ID."); return; }
                    if (int.TryParse(args[1], out int progId))
                    {
                        if (taskManager.ChangeStatus(progId, TaskItem.enStatus.In_Progress))
                            Console.WriteLine($"Task {progId} marked as in-progress.");
                        else
                            Console.WriteLine($"Task {progId} not found.");
                    }
                    else { Console.WriteLine("Error: Invalid ID format."); }
                    break;

                case "mark-done":
                    if (args.Length < 2) { Console.WriteLine("Error: Please provide a task ID."); return; }
                    if (int.TryParse(args[1], out int doneId))
                    {
                        if (taskManager.ChangeStatus(doneId, TaskItem.enStatus.Done))
                            Console.WriteLine($"Task {doneId} marked as done.");
                        else
                            Console.WriteLine($"Task {doneId} not found.");
                    }
                    else { Console.WriteLine("Error: Invalid ID format."); }
                    break;

                case "list":
                    TaskItem.enStatus statusToFilter = TaskItem.enStatus.All;
                    if (args.Length > 1)
                    {
                        string filter = args[1].ToLower();
                        if (filter == "done") statusToFilter = TaskItem.enStatus.Done;
                        else if (filter == "todo") statusToFilter = TaskItem.enStatus.Todo;
                        else if (filter == "in-progress") statusToFilter = TaskItem.enStatus.In_Progress;
                        else { Console.WriteLine("Error: Invalid list filter."); return; }
                    }

                    var tasks = taskManager.ListTasks(statusToFilter);
                    if (tasks.Count == 0)
                    {
                        Console.WriteLine("No Tasks Found.");
                    }
                    else
                    {
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"ID: {task.ID} | Desc: {task.Description} | Status: {task.Status} | Created: {task.CreatedAt}");
                        }
                    }
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }
}