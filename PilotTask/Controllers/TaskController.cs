using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLog;
using PilotTask.BusinessLogicLayers;
using PilotTask.Models;
using PilotTask.ViewModels.TasksViewModel;
using System.Threading.Tasks;

namespace PilotTask.Controllers
{
    public class TaskController : Controller
    {
        private TaskBusinessLogicLayer taskBusinessLogicLayer;
        private ProfileBusinessLogicLayer profileBusinessLogicLayer;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public TaskController(TaskBusinessLogicLayer taskBusinessLogicLayer, ProfileBusinessLogicLayer profileBusinessLogicLayer) 
        {
            this.taskBusinessLogicLayer = taskBusinessLogicLayer;
            this.profileBusinessLogicLayer = profileBusinessLogicLayer;

        }

        public IActionResult Index()
        {
            try
            {
                List<TaskViewModel> taskVsiewModels = taskBusinessLogicLayer.GetTasks();
                return View("Index", taskVsiewModels);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Index action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Details(int Id)
        {
            try
            {
                TaskViewModel taskViewModel = taskBusinessLogicLayer.GetTaskById(Id);

                return View("Details", taskViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Details action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Create(int profileId)
        {
            try
            {
                TaskCreateViewModel taskCreateViewModel = new TaskCreateViewModel
                {
                    Task = new TaskViewModel(),
                   
                    ProfileId = profileId
                };
                return View(taskCreateViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Create action.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(TaskCreateViewModel taskCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taskCreateViewModel.Task.ProfileId = taskCreateViewModel.ProfileId; // Ensure ProfileId is set
                    taskBusinessLogicLayer.AddTask(taskCreateViewModel.Task);
                    return RedirectToAction("Index", "Profile");
                }

                return View(taskCreateViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Create action.");
                return StatusCode(500, "Internal server error");
            }
        }


        public IActionResult Update(int Id)
        {
            try
            {
                TaskViewModel taskViewModel = taskBusinessLogicLayer.GetTaskById(Id);
                return View(taskViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Update action.");
                return StatusCode(500, "Internal server error");
            }
        }


            [HttpPost]
        public IActionResult Update(TaskViewModel taskViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    taskBusinessLogicLayer.UpdateTask(taskViewModel);

                    return RedirectToAction("Details", new { Id = taskViewModel.Id });
                }

                return View(taskViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Update action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Delete(int Id)
        {
            try
            {
                TaskViewModel taskViewModel = taskBusinessLogicLayer.GetTaskById(Id);
               
                TaskDeleteViewModel taskDeleteViewModel  = new TaskDeleteViewModel()
                {
                    task= taskViewModel ,
                    ProfileId = taskViewModel.ProfileId
                };
                return View(taskDeleteViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Delete action.");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public IActionResult DeleteConfirmed(TaskDeleteViewModel taskDeleteViewModel)
        {
            try
            {
               
                taskBusinessLogicLayer.DeleteTask(taskDeleteViewModel.task.Id);
                return RedirectToAction("Details", "Profile", new { ProfileId = taskDeleteViewModel.ProfileId });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in DeleteConfirmed action.");
                return StatusCode(500, "Internal server error");
            }


        }



    }
}
