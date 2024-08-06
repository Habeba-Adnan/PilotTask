using Microsoft.Data.SqlClient;
using System.Data;

namespace PilotTask.Helpers
{
    public class DatabaseHelper
    {
        private string ConnectionString = @"Data Source=DESKTOP-QLS20Q3;Initial Catalog=Pilot;Integrated Security=True;Trust Server Certificate=True";

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public void ExecuteNonQuery(string StoredProcedure , SqlParameter[] sqlParameters )
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

        public DataTable ExecuteQuery(string StoredProcedure, SqlParameter[] sqlParameters)
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
    }
}
