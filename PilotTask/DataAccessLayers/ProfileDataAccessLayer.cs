using System.Data;
using Microsoft.Data.SqlClient;
using PilotTask.Helpers;
using PilotTask.Models;
namespace PilotTask.DataAccessLayers
{
    public class ProfileDataAccessLayer
    {

        private DatabaseHelper databaseHelper;

        public ProfileDataAccessLayer(DatabaseHelper databaseHelper) 
        {
            this.databaseHelper =  databaseHelper;
        }
        
        public List<Profiles> GetProfiles()
        {
            DataTable ProfilesDataTable = databaseHelper.ExecuteQuery("SP_GetProfiles", null);
            if(ProfilesDataTable.Rows.Count==0)
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
        public Profiles GetProfileById(int ProfileId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@ProfileId",ProfileId)
            };
            DataTable ProfileDataTable = databaseHelper.ExecuteQuery("SP_GetProfileById", sqlParameters);
            if(ProfileDataTable.Rows.Count==0)
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
        public void AddProfile(Profiles profile)
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

        public void DeleteProfile(int ProfileId) 
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@ProfileId", ProfileId)
            };
            databaseHelper.ExecuteNonQuery("SP_DeleteProfile", sqlParameters);
        }

        public void UpdateProfile(Profiles profile) 
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
            databaseHelper.ExecuteNonQuery("SP_UpdateProfile",sqlParameters);
        }
    }
}
