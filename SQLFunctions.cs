using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace BusinessManagementStudioClassLibrary
{
    public class SQLFunctions
    {
        private string sqlConnectionString = "";

        /// <summary>
        /// Provides the basic SQL functionality.
        /// </summary>
        /// <param name="SQLConnectionString">The SQL connection string.</param>
        public SQLFunctions(string SQLConnectionString)
        {
            sqlConnectionString = SQLConnectionString;
        }
            
        /// <summary>
        /// Executes the given SQl query and loads all records into a Datatable.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>A Datatable with the query result set.</returns>
        public DataTable GetDatatableFromQuery(string sqlQuery)
        {
            DataTable dtResults = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {

                    //protect the database query from sql injection
                    sqlQuery = sqlQuery.Replace("'", "''");

                    //create an OleDbDataAdapter to execute the query
                    SqlDataAdapter dAdapter = new SqlDataAdapter(sqlQuery, conn);

                    //fill the DataTable
                    dAdapter.Fill(dtResults);
                }
            }
            catch
            {
                 //return the a blank datatable for now
            }

            return dtResults;
        }

        /// <summary>
        /// Executes the given SQL query and returns the first result from the query.
        /// </summary>
        /// <param name="sqlQuery">The SQL query</param>
        /// <returns>The first result from the query</returns>
        public object ExecuteScalarQuery(string sqlQuery)
        {
            try
            {
                object result;

                // create connection
                using (SqlConnection c = new SqlConnection(sqlConnectionString))
                {                   
                    //open connection
                    c.Open();
                    //build command
                    SqlCommand cmd = new SqlCommand(sqlQuery, c);
                    //execute 
                    result = cmd.ExecuteScalar();
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message; //return the actual error
            }
             
        }

        /// <summary>
        /// Executes the given SQL query and returns the number of records affected.
        /// </summary>
        /// <param name="sqlQuery">The SQL query</param>
        /// <returns>The number of records affected</returns>
        public int ExecuteNoneQuery(string sqlQuery)
        {
            try
            {
                int recordsCount = 0;

                // create connection
                using (SqlConnection c = new SqlConnection(sqlConnectionString))
                {
                    //open connection
                    c.Open();
                    //build command
                    SqlCommand cmd = new SqlCommand(sqlQuery, c);
                    //execute
                    recordsCount = cmd.ExecuteNonQuery();                    
                }

                return recordsCount;
            }
            catch
            {
                //reutrn -1 to indicate there was an error                
                return -1;
            }
        }
    }
}
