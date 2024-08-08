using PilotTask.Validations;
using System.ComponentModel.DataAnnotations;

namespace PilotTask.ViewModels.TasksViewModel
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }

        [Required(ErrorMessage = "Task Name is required.")]
        [StringLength(50, ErrorMessage = "Task Description cannot exceed 500 characters.")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Task Description is required.")]
        [StringLength(500, ErrorMessage = "Task Description cannot exceed 500 characters.")]
        public string TaskDescription { get; set; }

        [FutureDate(ErrorMessage = "Start Time must be a future date.")]
        [Required(ErrorMessage = "Start Time is required.")]
        public DateTime StartTime { get; set; }
        public TasksStatus Status { get; set; }
    }
}
