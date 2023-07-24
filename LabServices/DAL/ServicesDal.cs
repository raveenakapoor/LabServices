using LabServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabServices.DAL
{
    public class ServicesDal
    {
        public DataSet ShowLabServiceTest()
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            AddLabService objects = new AddLabService();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_show_LSP_Service";
                cmd.Connection = con;
               // cmd.Parameters.AddWithValue("@PatientId", PatientId);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                con.Close();
            }
        }


    }
}