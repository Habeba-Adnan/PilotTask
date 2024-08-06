using Microsoft.AspNetCore.Mvc;
using PilotTask.BusinessLogicLayers;
using PilotTask.Models;
using PilotTask.ViewModels;

namespace PilotTask.Controllers
{
    public class ProfileController : Controller
    {
        private ProfileBusinessLogicLayer profileBusinessLogicLayer;
        private TaskBusinessLogicLayer taskBusinessLogicLayer;
        public ProfileController() 
        {
            profileBusinessLogicLayer = new ProfileBusinessLogicLayer();
            taskBusinessLogicLayer = new TaskBusinessLogicLayer();
        }

        public IActionResult Index()
        {
            List<ProfileViewModel> profilesViewModels  = profileBusinessLogicLayer.GetProfiles();
            return View("Index", profilesViewModels);
        }

        public IActionResult Details(int ProfileId)
        {
            ProfileViewModel profileViewModel = profileBusinessLogicLayer.GetProfileById(ProfileId);

            List<TaskViewModel>  tasksViewModels = taskBusinessLogicLayer.GetTasksByProfileId(ProfileId);
            ViewBag.Tasks= tasksViewModels;
            return View("Details", profileViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProfileViewModel profileViewModel )
        {
            if (ModelState.IsValid)
            {
                profileBusinessLogicLayer.AddProfile(profileViewModel);
                return RedirectToAction("Index");
            }
            
            return View(profileViewModel);
        }

        public IActionResult Update(int ProfileId)
        {
            ProfileViewModel profile = profileBusinessLogicLayer.GetProfileById(ProfileId);
            List<TaskViewModel> tasksViewModels  = taskBusinessLogicLayer.GetTasksByProfileId(ProfileId);
            ViewBag.Tasks = tasksViewModels;
            return View(profile);
        }

        [HttpPost]
        public IActionResult Update(ProfileViewModel profileViewModel )
        {
            if (ModelState.IsValid)
            {
                profileBusinessLogicLayer.UpdateProfile(profileViewModel);
                return RedirectToAction("Index");
            }

            return View(profileViewModel);
        }

        public IActionResult Delete(int ProfileId)
        {
            ProfileViewModel profileViewModel = profileBusinessLogicLayer.GetProfileById(ProfileId);
            return View(profileViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int ProfileId)
        {
          
            profileBusinessLogicLayer.DeleteProfile(ProfileId);
            return RedirectToAction("Index");
           
        }


    }
}
