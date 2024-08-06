using Microsoft.Data.SqlClient;
using PilotTask.Enums;
using PilotTask.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
namespace PilotTask.DataAccessLayers
{
    public class TaskDataAccessLayer
    {
        private DatabaseHelper databaseHelper;

        public TaskDataAccessLayer()
        {
            databaseHelper = new DatabaseHelper();
        }

        public List<Tasks> GetTasks()
        {
            DataTable TasksDataTable = databaseHelper.ExecuteQuery("SP_GetTasks", null);
            if (TasksDataTable.Rows.Count == 0)
            {
                return null;
            }
            List<Tasks> Tasks = new List<Tasks>();
            foreach (DataRow Row in TasksDataTable.Rows)
            {
                Tasks.Add(new Tasks
                {
                    Id = Convert.ToInt32(Row["Id"]),
                    ProfileId = Convert.ToInt32(Row["ProfileId"]),
                    TaskName = Row["TaskName"].ToString(),
                    TaskDescription = Row["TaskDescription"].ToString(),
                    StartTime = Convert.ToDateTime(Row["StartTime"]),
                    Status = (PilotTask.Enums.TasksStatus)Convert.ToInt32(Row["Status"]),
                    
                });

            }
            return Tasks;
        }

        public Tasks GetTaskById(int Id)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id",Id)
            };
            DataTable TaskDataTable = databaseHelper.ExecuteQuery("SP_GetTaskById", sqlParameters);
            if (TaskDataTable.Rows.Count == 0)
            {
                return null;
            }
            //Tasks task = new Tasks();
            DataRow Row = TaskDataTable.Rows[0];
            return new Tasks()
            {
                Id = Convert.ToInt32(Row["Id"]),
                ProfileId = Convert.ToInt32(Row["ProfileId"]),
                TaskName = Row["TaskName"].ToString(),
                TaskDescription = Row["TaskDescription"].ToString(),
                StartTime = Convert.ToDateTime(Row["StartTime"]),
                Status = (PilotTask.Enums.TasksStatus)Convert.ToInt32(Row["Status"]),

            };
        }

        public List<Tasks> GetTasksByProfileId(int ProfileId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@ProfileId",ProfileId)
            };
            DataTable TasksDataTable = databaseHelper.ExecuteQuery("SP_GetTasksByProfileId", sqlParameters);
            if (TasksDataTable.Rows.Count == 0)
            {
                return null;
            }
            List<Tasks> Tasks = new List<Tasks>();
            foreach (DataRow Row in TasksDataTable.Rows)
            {
                Tasks.Add(new Tasks
                {
                    Id = Convert.ToInt32(Row["Id"]),
                    ProfileId = Convert.ToInt32(Row["ProfileId"]),
                    TaskName = Row["TaskName"].ToString(),
                    TaskDescription = Row["TaskDescription"].ToString(),
                    StartTime = Convert.ToDateTime(Row["StartTime"]),
                    Status = (PilotTask.Enums.TasksStatus)Convert.ToInt32(Row["Status"]),

                });

            }
            return Tasks;
        }

        public void AddTask(Tasks task )
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@ProfileId",task.ProfileId),
                new SqlParameter("@TaskName", task.TaskName),
                new SqlParameter("@TaskDescription",task.TaskDescription),
                new SqlParameter("@StartTime",task.StartTime),
                new SqlParameter("@Status",(int)task.Status)

            };
            databaseHelper.ExecuteNonQuery("SP_AddTask", sqlParameters);
        }

        public void DeleteTask(int Id)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id", Id)
            };
            databaseHelper.ExecuteNonQuery("SP_DeleteTask", sqlParameters);
        }

        public void UpdateTask(Tasks task )
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
           {
                new SqlParameter("@Id",task.Id),
                new SqlParameter("@ProfileId",task.ProfileId),
                new SqlParameter("@TaskName", task.TaskName),
                new SqlParameter("@TaskDescription",task.TaskDescription),
                new SqlParameter("@StartTime",task.StartTime),
                new SqlParameter("@Status",(int)task.Status)

           };
            databaseHelper.ExecuteNonQuery("SP_UpdateTask", sqlParameters);
        }


    }
}
