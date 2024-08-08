using Microsoft.Data.SqlClient;
using NLog;
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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public TaskDataAccessLayer(DatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public List<Tasks> GetTasks()
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetTasks method");
                throw;
            }

        }
    
        public Tasks GetTaskById(int Id)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetTaskById method");
                throw;
            }

        }

        public List<Tasks> GetTasksByProfileId(int ProfileId)
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error in GetTasksByProfileId method");
                throw;
            }


        }

        public void AddTask(Tasks task )
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error in AddTask method");
                throw;
            }

        }

        public void DeleteTask(int Id)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
          {
                new SqlParameter("@Id", Id)
          };
                databaseHelper.ExecuteNonQuery("SP_DeleteTask", sqlParameters);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in DeleteTask method");
                throw;
            }

        }

        public void UpdateTask(Tasks task )
        {
            try
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
            catch (Exception ex)
            {
                logger.Error(ex, "Error in UpdateTask method");
                throw;
            }

        }


    }
}
