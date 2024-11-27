using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AFAM_Management.DAL
{
    class DataAccessLayer
    {
        SqlConnection sqlconnection;
        // This constructor initialize the Connection object 
        public DataAccessLayer()
        {   /* If we are using username and password in our sql we need to change the Security to false 
            and add more to parameter ID and password (you can search for it in Goodgle) */
            sqlconnection = new SqlConnection(@"Server= .\SQLEXPRESS; Database=Product_DB; Integrated Security=True"); // Connection line
        }                                                                   //              Security=fales; ID=user1;Password=123456;)
        // Method to open the Connection (بدنا نفتح إجراء والإجراءات هي رح تكون ب فويد void)
        public void Open()
        {
            if (sqlconnection.State != ConnectionState.Open)
            {
                sqlconnection.Open();
            }
        }
        // Method to close the Connection. 
        public void Close()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }
        // Method to Read the data from our database, and here we need to Use function not procedures
        public DataTable SelectData(string stored_procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            // sqlDataAdapter is reading the data and store it id datatable and return it
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        // Method to ADD, UPDATE and DELETE  data from Datebase 
        public void ExecuteCommand(string stored_procedure, SqlParameter[] param)
        {
            // Here we didn't use SqlDataAdapter  Because we don't want to read data or return them. 
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                // Add (Adds one value) | AddRagne (Adds an array)
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
    }
}
