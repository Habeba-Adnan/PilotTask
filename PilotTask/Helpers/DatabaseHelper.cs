using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace PilotTask.Helpers
{
    public class DatabaseHelper
    {
      
        private readonly string connectionString;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DatabaseHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }


        public void ExecuteNonQuery(string StoredProcedure , SqlParameter[] sqlParameters )
        {
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    using (SqlCommand sqlCommand = new SqlCommand(StoredProcedure, sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        if (sqlParameters != null)
                        {
                            sqlCommand.Parameters.AddRange(sqlParameters);
                        }
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error executing non-query stored procedure");
                throw;
               
            }

        }

        public DataTable ExecuteQuery(string StoredProcedure, SqlParameter[] sqlParameters)
        {
            try
            {
                using (SqlConnection sqlConnection = GetSqlConnection())
                {
                    using (SqlCommand sqlCommand = new SqlCommand(StoredProcedure, sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        if (sqlParameters != null)
                        {
                            sqlCommand.Parameters.AddRange(sqlParameters);
                        }

                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error executing query stored procedure");
                throw;
            }

        }
    }
}
