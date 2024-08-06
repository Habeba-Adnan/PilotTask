using AutoMapper;
using PilotTask.DataAccessLayers;
using PilotTask.Models;
using PilotTask.ViewModels;

namespace PilotTask.BusinessLogicLayers
{
    public class ProfileBusinessLogicLayer
    {
        private ProfileDataAccessLayer profileDataAccessLayer;
        public ProfileBusinessLogicLayer()
        {
            profileDataAccessLayer = new ProfileDataAccessLayer();
        }

        public void AddProfile(ProfileViewModel profileViewModel)
        {
            try
            {
                Profiles profile = new Profiles
                {
                    ProfileId = profileViewModel.ProfileId,
                    FirstName = profileViewModel.FirstName,
                    LastName = profileViewModel.LastName,
                    DateOfBirth = profileViewModel.DateOfBirth,
                    PhoneNumber = profileViewModel.PhoneNumber,
                    EmailId = profileViewModel.EmailId
                };

                profileDataAccessLayer.AddProfile(profile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the profile.", ex);
            }
        }

        public void DeleteProfile(int ProfileId)
        {
            try
            {
                profileDataAccessLayer.DeleteProfile(ProfileId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the profile.", ex);
            }
        }

        public void UpdateProfile(ProfileViewModel profileViewModel)
        {
            try
            {
                Profiles profile = new Profiles
                {
                    FirstName = profileViewModel.FirstName,
                    LastName= profileViewModel.LastName,
                    DateOfBirth= profileViewModel.DateOfBirth,
                    PhoneNumber= profileViewModel.PhoneNumber,
                    EmailId= profileViewModel.EmailId,
                    ProfileId= profileViewModel.ProfileId
                };
                profileDataAccessLayer.UpdateProfile(profile);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while Updating the profile.", ex);
            }
        }

        public List<ProfileViewModel> GetProfiles()
        {
            try
            {
                List<Profiles> profiles = profileDataAccessLayer.GetProfiles();
                // Convert List<Profiles> to List<ProfileViewModel>
                return profiles.Select(p => new ProfileViewModel
                {
                    ProfileId = p.ProfileId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    PhoneNumber = p.PhoneNumber,
                    EmailId = p.EmailId
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving all profiles.", ex);
            }
        }

        public ProfileViewModel GetProfileById(int ProfileId) 
        {
            try
            {
                Profiles profile=  profileDataAccessLayer.GetProfileById(ProfileId);
                if (profile == null) return null;
                return new ProfileViewModel
                {
                    ProfileId = profile.ProfileId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    DateOfBirth = profile.DateOfBirth,
                    PhoneNumber = profile.PhoneNumber,
                    EmailId = profile.EmailId
                };
            }
            catch (Exception ex) {
                throw new ApplicationException("An error occurred while Retrieving the profile.", ex);
            }
        }
    }
}
