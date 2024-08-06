using PilotTask.Enums;

namespace PilotTask.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public TasksStatus Status { get; set; }

    }
}
