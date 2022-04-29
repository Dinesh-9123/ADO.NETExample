using SolrNet;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseConnection
{
    public class SqlHelper
    {
        private SqlConnection dbConnection = new SqlConnection();

        public SqlConnection Dbconnection()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = ConfigurationManager.AppSettings["CraftModaConnection"];
            return Conn;
        }
        public string GetConnectionString()
        {
            return Dbconnection().ConnectionString;
        }
        private void OpenConnection()
        {
            dbConnection = Dbconnection();
            dbConnection.Open();
        }
        private void CloseConnection()
        {
            dbConnection.Close();
        }

        public int DMLMethod(string strProcName, AddParameters sql)
        {
            int result = 0;
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(strProcName, dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < 5; i++)
                {
                    //dbCommand.Parameters.Add(sql[i]);
                }
                result = dbCommand.ExecuteNonQuery();
                CloseConnection();


            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {

                CloseConnection();
            }
            return result;

        }

        public DataSet GetDataSet(String ProcedureName, AddParameters SqlParameter)
        {
            DataSet dtl = new DataSet();
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(ProcedureName, dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3600;
                for (int i = 0; i < 5; i++)
                {
                   // dbCommand.Parameters.Add(SqlParameter[i]);
                }
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbCommand);
                dbAdapter.Fill(dtl);
                CloseConnection();
            }
            catch (Exception ex)
            {
                //throw (ex);
            }
            finally { CloseConnection(); }
            return dtl;

        }

        public object ExecuteScaler(string strProcName, AddParameters sql)
        {
            int affectedRows = 0;
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(strProcName, dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < 5; i++)
                {
                   // dbCommand.Parameters.Add(sql[i]);
                }
                object result = dbCommand.ExecuteScalar();
                if (result != null && result.GetType() != typeof(DBNull))
                {
                    affectedRows = Convert.ToInt32(result);
                }
                CloseConnection();
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {

                CloseConnection();
            }
            return affectedRows;

        }

        public object ExecuteScaler(string strQuery)
        {
            object result = 0;
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(strQuery, dbConnection);
                dbCommand.CommandType = CommandType.Text;
                result = dbCommand.ExecuteScalar();
                CloseConnection();


            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {

                CloseConnection();
            }
            return result;

        }

        public int ExecuteNonQuery(String strQuery)
        {
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(strQuery, dbConnection);
                dbCommand.CommandType = CommandType.Text;
                int result = dbCommand.ExecuteNonQuery();
                CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
            finally { CloseConnection(); }

        }


        public DataTable GetDataTable(String ProcedureName, AddParameters SqlParameter)
        {
            DataTable dtl = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(ProcedureName, dbConnection);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandTimeout = 3600;
                for (int i = 0; i < 5; i++)
                {
                   // dbCommand.Parameters.Add(SqlParameter[i]);
                }
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbCommand);
                dbAdapter.Fill(dtl);
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally { CloseConnection(); }
            return dtl;
        }

        public DataTable GetDataTable(String QueryString)
        {
            DataTable dtl = new DataTable();
            try
            {
                OpenConnection();
                SqlCommand dbCommand = new SqlCommand(QueryString, dbConnection);
                dbCommand.CommandType = CommandType.Text;
                SqlDataAdapter dbAdapter = new SqlDataAdapter(dbCommand);
                dbAdapter.Fill(dtl);
                CloseConnection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { CloseConnection(); }
            return dtl;
        }

        public void Dispose()
        {
            if (dbConnection != null)
            {
                dbConnection.Dispose();
            }
        }
    }
}