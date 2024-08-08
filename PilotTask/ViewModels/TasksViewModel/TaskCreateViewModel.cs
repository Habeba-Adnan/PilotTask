namespace PilotTask.ViewModels.TasksViewModel
{
    public class TaskCreateViewModel
    {
        public TaskViewModel Task { get; set; } // Changed 'task' to 'Task' to follow C# naming conventions
        public int ProfileId { get; set; }
    }
}
