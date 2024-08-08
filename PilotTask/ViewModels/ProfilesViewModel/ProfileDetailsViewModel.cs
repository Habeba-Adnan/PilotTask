using PilotTask.ViewModels.TasksViewModel;

namespace PilotTask.ViewModels.ProfilesViewModel
{
    public class ProfileDetailsViewModel
    {
        public ProfileViewModel profile { get; set; }
        public List<TaskViewModel> tasks { get; set; }
    }
}
