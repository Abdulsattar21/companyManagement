using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using AFAM_Management.DAL;
using System.Runtime.CompilerServices;

namespace AFAM_Management.BL
{
    class CLS_LOGIN
    {
        // This class is about the LOGIN 
        public DataTable LOGIN(string ID, String PWD)
        {
            // Here we bring a new object from DateAccessLayer and Copy it
            DataAccessLayer DAL = new DAL.DataAccessLayer();
            // Now we create a new Arrey to store the pass and the username so we can use them later
            SqlParameter[] param = new SqlParameter[2];
            // first one is the ID
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            // Second one is the pass 
            param[1] = new SqlParameter("@PWD", SqlDbType.VarChar, 50);
            param[1].Value = PWD;
            // here we open the connection with the database so we can check the ID & PASS
            DAL.Open();
            // we create a DateTable
            DataTable dt = new DataTable();
            dt = DAL.SelectData("SP_LOGIN", param);
            DAL.Close();
            // Check the Stored Procedure and return the retule if it is TRUE or FALSE. 
            return dt;
        }
    }
}
