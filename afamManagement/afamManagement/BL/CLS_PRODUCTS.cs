using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using AFAM_Management.DAL;

namespace afamManagement.BL
{   
    class CLS_PRODUCTS
    {
        // Here we just copied This cod from CLS_LOGIN Class.
        
        public DataTable GET_ALL_CATEGORIES()
        {
            DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("GET_ALL_CATEGORIES", null);
            DAL.Close();
            return dt;
        }
        public void ADD_PRODUCT(int ID_CAT, string Label_Product, string ID_Product,
                            int Qte, string Price, byte[] img)
        {
            AFAM_Management.DAL.DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[6];
            PL.FRM_ADD_PRODUCT frm = new PL.FRM_ADD_PRODUCT();
            
            param[0] = new SqlParameter("@ID_CAT", SqlDbType.Int);
            param[0].Value = ID_CAT;

            param[1] = new SqlParameter("@ID_PRODUCT", SqlDbType.VarChar, 30);
            param[1].Value = ID_Product;

            param[2] = new SqlParameter("@Label", SqlDbType.VarChar, 30);
            param[2].Value = Label_Product;

            param[3] = new SqlParameter("@Qte", SqlDbType.Int);
            param[3].Value = Qte;

            param[4] = new SqlParameter("@PRICE ", SqlDbType.VarChar, 50);
            param[4].Value = Price;

            param[5] = new SqlParameter("@IMG ", SqlDbType.Image);
            param[5].Value = img;
            DAL.ExecuteCommand("ADD_PRODUCT", param);
            DAL.Close();
        }

        public void UPDATE_PRODUCT(int ID_CAT, string Label_Product, string ID_Product,
                          int Qte, string Price, byte[] img)
        {
            AFAM_Management.DAL.DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[6];
            PL.FRM_ADD_PRODUCT frm = new PL.FRM_ADD_PRODUCT();

            param[0] = new SqlParameter("@ID_CAT", SqlDbType.Int);
            param[0].Value = ID_CAT;

            param[1] = new SqlParameter("@ID_PRODUCT", SqlDbType.VarChar, 30);
            param[1].Value = ID_Product;

            param[2] = new SqlParameter("@Label", SqlDbType.VarChar, 30);
            param[2].Value = Label_Product;

            param[3] = new SqlParameter("@Qte", SqlDbType.Int);
            param[3].Value = Qte;

            param[4] = new SqlParameter("@PRICE ", SqlDbType.VarChar, 50);
            param[4].Value = Price;

            param[5] = new SqlParameter("@IMG ", SqlDbType.Image);
            param[5].Value = img;
            DAL.ExecuteCommand("UPDATE_PRODUCT", param);
            DAL.Close();
        }
        public DataTable verifyProductID(string ID)
        {
            DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            dt = DAL.SelectData("verifyProductID", param);
            DAL.Close();
            return dt;
        }
        public void DeleteProduct(int ID)
        {
            AFAM_Management.DAL.DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;

            DAL.ExecuteCommand("DeleteProduct", param);
            DAL.Close();
        }
        public DataTable GET_ALL_PRODUCTS()
        {
            DataAccessLayer DAL = new DataAccessLayer();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("GET_ALL_PRODUCTS", null);
            return dt;
        }
        public DataTable SearchProducts(string ID)
        {
            DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            dt = DAL.SelectData("SearchProducts", param);
            DAL.Close();
            return dt;
        }
        public DataTable GET_IMAGE_PRRODUCT(string ID)
        {
            DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            DataTable dt = new DataTable();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            dt = DAL.SelectData("GET_IMAGE_PRRODUCT", param);
            DAL.Close();
            return dt;
        }
        public void DeleteProduct(string ID)
        {
            AFAM_Management.DAL.DataAccessLayer DAL = new DataAccessLayer();
            DAL.Open();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", SqlDbType.VarChar, 50);
            param[0].Value = ID;
            DAL.ExecuteCommand("DeleteProduct", param);
            DAL.Close();
        }

    }

    }
