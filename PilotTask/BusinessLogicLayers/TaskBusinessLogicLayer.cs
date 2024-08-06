using PilotTask.DataAccessLayers;
using PilotTask.Models;

namespace PilotTask.BusinessLogicLayers
{
    public class TaskBusinessLogicLayer
    {
        private TaskDataAccessLayer taskDataAccessLayer;
        public TaskBusinessLogicLayer() 
        {
            taskDataAccessLayer = new TaskDataAccessLayer();
        }
        public void AddTask(TaskViewModel taskViewModel)
        {
            try
            {
                Tasks task = new Tasks 
                {
                    ProfileId = taskViewModel.ProfileId,
                    Id= taskViewModel.Id,
                    StartTime= taskViewModel.StartTime,
                    TaskName= taskViewModel.TaskName,
                    TaskDescription= taskViewModel.TaskDescription,
                    Status= taskViewModel.Status
                };
                taskDataAccessLayer.AddTask(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the task.", ex);
            }
        }
        public void DeleteTask(int Id)
        {
            try
            {
                taskDataAccessLayer.DeleteTask(Id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the task.", ex);
            }
        }
        public void UpdateTask(TaskViewModel taskViewModel)
        {
            try
            {
                Tasks task = new Tasks
                {
                    ProfileId = taskViewModel.ProfileId,
                    Id = taskViewModel.Id,
                    StartTime = taskViewModel.StartTime,
                    TaskName = taskViewModel.TaskName,
                    TaskDescription = taskViewModel.TaskDescription,
                    Status = taskViewModel.Status
                };

                taskDataAccessLayer.UpdateTask(task);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Updating the task.", ex);
            }
        }
        public List<TaskViewModel> GetTasks()
        {
            try
            {
                List<Tasks> tasks= taskDataAccessLayer.GetTasks();
                return tasks?.Select(t => new TaskViewModel
                {
                    ProfileId = t.ProfileId,
                    Id= t.Id,
                    TaskName= t.TaskName,
                    TaskDescription= t.TaskDescription,
                    StartTime=t.StartTime,
                    Status= t.Status,       
                   
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Retrieving all tasks.", ex);
            }
        }

        public TaskViewModel GetTaskById(int Id)
        {
            try
            {
                Tasks task= taskDataAccessLayer.GetTaskById(Id);
                if(task == null)
                {
                    return null; 
                }
                return new TaskViewModel
                {
                    Id= task.Id,
                    TaskName= task.TaskName,
                    TaskDescription= task.TaskDescription,
                    ProfileId= task.ProfileId,
                    StartTime= task.StartTime,
                    Status= task.Status
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Retrieving the task.", ex);
            }
        }

        public List<TaskViewModel> GetTasksByProfileId(int ProfileId)
        {
            try
            {
                List<Tasks> tasks= taskDataAccessLayer.GetTasksByProfileId(ProfileId);
                return tasks?.Select(t => new TaskViewModel
                {
                    ProfileId = t.ProfileId,
                    Id = t.Id,
                    TaskName = t.TaskName,
                    TaskDescription = t.TaskDescription,
                    StartTime = t.StartTime,
                    Status = t.Status,

                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Retrieving all tasks.", ex);
            }
        }

    }
}
