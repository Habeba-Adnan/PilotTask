using NLog;
using PilotTask.DataAccessLayers;
using PilotTask.Models;
using PilotTask.ViewModels.TasksViewModel;

namespace PilotTask.BusinessLogicLayers
{
    public class TaskBusinessLogicLayer
    {
        private TaskDataAccessLayer taskDataAccessLayer;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public TaskBusinessLogicLayer(TaskDataAccessLayer taskDataAccessLayer) 
        {
            this.taskDataAccessLayer = taskDataAccessLayer;
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
               logger.Error(ex,"An error occurred while adding the task.");
                throw;
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
                logger.Error(ex,"An error occurred while deleting the task.");
                throw;
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
                logger.Error(ex,"An error occurred while Updating the task.");
                throw;
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
               logger.Error
                    (ex,"An error occurred while Retrieving all tasks.");
                throw; 
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
               logger.Error(ex,"An error occurred while Retrieving the task.");
                throw;
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
                logger.Error(ex,"An error occurred while Retrieving all tasks.");
                throw;
            }
        }

    }
}
