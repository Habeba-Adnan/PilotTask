using System.Data;
using Microsoft.Data.SqlClient;
using NLog;
using PilotTask.Helpers;
using PilotTask.Models;
namespace PilotTask.DataAccessLayers
{
    public class ProfileDataAccessLayer
    {

        private DatabaseHelper databaseHelper;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProfileDataAccessLayer(DatabaseHelper databaseHelper) 
        {
            this.databaseHelper =  databaseHelper;
        }
        
        public List<Profiles> GetProfiles()
        {
            try
            {
                DataTable ProfilesDataTable = databaseHelper.ExecuteQuery("SP_GetProfiles", null);
                if (ProfilesDataTable.Rows.Count == 0)
                {
                    return null;
                }
                List<Profiles> profiles = new List<Profiles>();
                foreach (DataRow Row in ProfilesDataTable.Rows)
                {
                    profiles.Add(new Profiles
                    {
                        ProfileId = Convert.ToInt32(Row["ProfileId"]),
                        FirstName = Row["FirstName"].ToString(),
                        LastName = Row["LastName"].ToString(),
                        PhoneNumber = Row["PhoneNumber"].ToString(),
                        DateOfBirth = Convert.ToDateTime(Row["DateOfBirth"]),
                        EmailId = Row["EmailId"].ToString()
                    });

                }
                return profiles;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetProfiles method");
                throw;
            }
        }
        public Profiles GetProfileById(int ProfileId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@ProfileId",ProfileId)
            };
                DataTable ProfileDataTable = databaseHelper.ExecuteQuery("SP_GetProfileById", sqlParameters);
                if (ProfileDataTable.Rows.Count == 0)
                {
                    return null;
                }
                //Profiles profile = new Profiles();
                DataRow Row = ProfileDataTable.Rows[0];
                return new Profiles()
                {
                    ProfileId = Convert.ToInt32(Row["ProfileId"]),
                    FirstName = Row["FirstName"].ToString(),
                    LastName = Row["LastName"].ToString(),
                    PhoneNumber = Row["PhoneNumber"].ToString(),
                    DateOfBirth = Convert.ToDateTime(Row["DateOfBirth"]),
                    EmailId = Row["EmailId"].ToString()
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetProfileById method");
                throw;
            }

        }
        public void AddProfile(Profiles profile)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
          {
                new SqlParameter("@FirstName",profile.FirstName),
                new SqlParameter("@LastName", profile.LastName),
                new SqlParameter("@PhoneNumber",profile.PhoneNumber),
                new SqlParameter("@DateOfBirth",profile.DateOfBirth),
                new SqlParameter("@EmailId",profile.EmailId)

          };
                databaseHelper.ExecuteNonQuery("SP_AddProfile", sqlParameters);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error in AddProfile method");
                throw;
            }
          
        }

        public void DeleteProfile(int ProfileId) 
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@ProfileId", ProfileId)
           };
                databaseHelper.ExecuteNonQuery("SP_DeleteProfile", sqlParameters);
            }
            catch (Exception ex) 
            {
                logger.Error(ex, "Error in DeleteProfile method");
                throw;
            }
           
        }

        public void UpdateProfile(Profiles profile) 
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
          {
               new SqlParameter("@ProfileID",profile.ProfileId),
                new SqlParameter("@FirstName",profile.FirstName),
                new SqlParameter("@LastName", profile.LastName),
                new SqlParameter("@PhoneNumber",profile.PhoneNumber),
                new SqlParameter("@DateOfBirth",profile.DateOfBirth),
                new SqlParameter("@EmailId",profile.EmailId)

          };
                databaseHelper.ExecuteNonQuery("SP_UpdateProfile", sqlParameters);
            }
            catch(Exception ex)
            {
                logger.Error(ex, " Error in UpdateProfile method");
                throw;
            }
           
        }
    }
}
