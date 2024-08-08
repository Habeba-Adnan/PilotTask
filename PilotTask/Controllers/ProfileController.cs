using Microsoft.AspNetCore.Mvc;
using NLog;
using PilotTask.BusinessLogicLayers;
using PilotTask.Models;
using PilotTask.ViewModels.ProfilesViewModel;
using PilotTask.ViewModels.TasksViewModel;

namespace PilotTask.Controllers
{
    public class ProfileController : Controller
    {
        private ProfileBusinessLogicLayer profileBusinessLogicLayer;
        private TaskBusinessLogicLayer taskBusinessLogicLayer;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProfileController(ProfileBusinessLogicLayer profileBusinessLogicLayer, TaskBusinessLogicLayer taskBusinessLogicLayer) 
        {
            this.profileBusinessLogicLayer = profileBusinessLogicLayer;
            this.taskBusinessLogicLayer = taskBusinessLogicLayer;
        }

        public IActionResult Index()
        {
            try
            {
                List<ProfileViewModel> profilesViewModels = profileBusinessLogicLayer.GetProfiles();
                return View("Index", profilesViewModels);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Index action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Details(int ProfileId)
        {
            try
            {
                ProfileViewModel profileViewModel = profileBusinessLogicLayer.GetProfileById(ProfileId);
                List<TaskViewModel> tasksViewModels = taskBusinessLogicLayer.GetTasksByProfileId(ProfileId);

                ProfileDetailsViewModel profileDetailsViewModel  = new ProfileDetailsViewModel()
                {
                    profile = profileViewModel,
                    tasks = tasksViewModels
                };

                return View("Details", profileDetailsViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Details action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Create()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Create action.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Create(ProfileViewModel profileViewModel )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    profileBusinessLogicLayer.AddProfile(profileViewModel);
                    return RedirectToAction("Index");
                }

                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Create action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Update(int ProfileId)
        {
            try
            {
                ProfileViewModel profile = profileBusinessLogicLayer.GetProfileById(ProfileId);
                List<TaskViewModel> tasksViewModels = taskBusinessLogicLayer.GetTasksByProfileId(ProfileId);
               
                return View(profile);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Update action.");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public IActionResult Update(ProfileViewModel profileViewModel )
        {
            try
            {

                if (ModelState.IsValid)
                {
                    profileBusinessLogicLayer.UpdateProfile(profileViewModel);
                    return RedirectToAction("Index");
                }

                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Update action.");
                return StatusCode(500, "Internal server error");
            }

        }

        public IActionResult Delete(int ProfileId)
        {
            try
            {
                ProfileViewModel profileViewModel = profileBusinessLogicLayer.GetProfileById(ProfileId);
                return View(profileViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in Delete action.");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int ProfileId)
        {
            try
            {
                profileBusinessLogicLayer.DeleteProfile(ProfileId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in DeleteConfirmed action.");
                return StatusCode(500, "Internal server error");
            }


        }


    }
}
