using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITaskManager
    {
        TaskItem AddNewTask(string description);
        bool UpdateTask(int id, string newDescription);
        bool DeleteTask(int id);
        bool ChangeStatus(int id, TaskItem.enStatus newStatus);
        List<TaskItem> ListTasks(TaskItem.enStatus filterStatus = TaskItem.enStatus.All);
    }
}
