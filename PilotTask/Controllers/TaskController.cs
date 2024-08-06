using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PilotTask.BusinessLogicLayers;
using PilotTask.Models;
using PilotTask.ViewModels;
using System.Threading.Tasks;

namespace PilotTask.Controllers
{
    public class TaskController : Controller
    {
        private TaskBusinessLogicLayer taskBusinessLogicLayer;
        private ProfileBusinessLogicLayer profileBusinessLogicLayer;
        public TaskController() 
        {
            taskBusinessLogicLayer = new TaskBusinessLogicLayer();
            profileBusinessLogicLayer = new ProfileBusinessLogicLayer();

        }

        public IActionResult Index()
        {
            List<TaskViewModel> taskVsiewModels  = taskBusinessLogicLayer.GetTasks();
            return View("Index", taskVsiewModels);
        }

        public IActionResult Details(int Id)
        {
            TaskViewModel taskViewModel  = taskBusinessLogicLayer.GetTaskById(Id);

            return View("Details", taskViewModel);
        }

        public IActionResult Create(int profileId)
        {
            ViewBag.ProfileId = profileId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                taskBusinessLogicLayer.AddTask(taskViewModel);
                return RedirectToAction("Index","Profile");
            }

            ViewBag.ProfileId = taskViewModel.ProfileId;
            return View(taskViewModel);
        }

        public IActionResult Update(int Id)
        {
            TaskViewModel taskViewModel  = taskBusinessLogicLayer.GetTaskById(Id);
            return View(taskViewModel);
        }

        [HttpPost]
        public IActionResult Update(TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                taskBusinessLogicLayer.UpdateTask(taskViewModel);
                
                return RedirectToAction("Details", new { Id = taskViewModel.Id });
            }

            return View(taskViewModel);
        }

        public IActionResult Delete(int Id)
        {
            TaskViewModel taskViewModel  = taskBusinessLogicLayer.GetTaskById(Id);
            TempData["ProfileId"] = taskViewModel.ProfileId;
            return View(taskViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int Id)
        {
            int ProfileId = (int)TempData["ProfileId"];
            taskBusinessLogicLayer.DeleteTask(Id);
            return RedirectToAction("Details","Profile", new { ProfileId = ProfileId });

        }



    }
}
