using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LabServices.Models;

namespace LabServices.DAL
{
    public class LSPDal
    {
        public DataSet GetData(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetUserData";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Userid", UserId);
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
        public int DeleteLsp(string userid)
        {
            SqlConnection con = null;
            int result = 0;
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Deletelsp";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Userid", userid);
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                con.Close();
            }
        }
        public int DeleteService(string testcode)
        {
            SqlConnection con = null;
            int result = 0;
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Deleteservice";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@testcode", testcode);
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
            finally
            {
                con.Close();
            }
        }
        public DataSet GetServices(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetServices";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Userid", UserId);
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

        public DataSet GetServiceSchedule(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.CommandText = "GetServicesForSchedule";
                cmd.CommandText = "GetServices";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@userid", UserId);
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
        public DataSet GetServiceId(string testcode)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
               cmd.CommandText = "GetServicesid";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@testcode", testcode);
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
        public DataSet GetAnotherLSP(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            LSPModel objects = new LSPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAnotherLSP";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Userid", UserId);
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
        public LSPModel GetState(int zipcode)
        {
            SqlConnection con = null;
            LSPModel objects = new LSPModel();
            int count = 0;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCity";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@zipcode", zipcode);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    objects.City = reader["City"].ToString();
                    objects.State = reader["State"].ToString();
                    count++;
                }
                if (count == 0)
                {
                    objects.ZipCode = 1;
                }
                return objects;
            }
            catch (Exception ex)
            {
                return objects;
            }
            finally
            {
                con.Close();
            }
        }
        public string insert_LSP(LSPModel ob)
        {

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Register_LSP";
                cmd.Connection = con;
                
                 cmd.Parameters.AddWithValue("@Parentid", ob.ParentId);
                cmd.Parameters.AddWithValue("@name", ob.Name);
                cmd.Parameters.AddWithValue("@Address1", ob.Address);
                cmd.Parameters.AddWithValue("@zipcode", ob.ZipCode);
                cmd.Parameters.AddWithValue("@city", ob.City);
                cmd.Parameters.AddWithValue("@state1", ob.State);
                cmd.Parameters.AddWithValue("@contact_no", ob.ContactNo);
                cmd.Parameters.AddWithValue("@alternate_no", ob.AltContactNo);
                cmd.Parameters.AddWithValue("@email", ob.Email);
                cmd.Parameters.AddWithValue("@category", ob.Category);
                cmd.Parameters.AddWithValue("@FavActor", ob.Answer1);
                cmd.Parameters.AddWithValue("@FavFood", ob.Answer2);
                cmd.Parameters.AddWithValue("@BirthCity", ob.Answer2);
                cmd.Parameters.AddWithValue("@Password", ob.Passsword);
                cmd.Parameters.AddWithValue("@UserType", "LSP");
                cmd.Parameters.Add("@LabId", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //   cmd.Parameters["@LabId"].Direction = ParameterDirection.Output;
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return "1";
                    //not inseretd successfully
                }
                else
                {
                    string labid = (cmd.Parameters["@LabId"].Value).ToString();
                    return (labid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "0";
            }
            finally
            {
                con.Close();
            }
        }

        public int UpdateDAL(LSPModel ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Update_LSP";
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@UserID", ob.LabId);
                cmd.Parameters.AddWithValue("@name", ob.Name);
                cmd.Parameters.AddWithValue("@Address1", ob.Address);
                cmd.Parameters.AddWithValue("@zipcode", ob.ZipCode);
                cmd.Parameters.AddWithValue("@city", ob.City);
                cmd.Parameters.AddWithValue("@state1", ob.State);
                cmd.Parameters.AddWithValue("@contact_no", ob.ContactNo);
                cmd.Parameters.AddWithValue("@alternate_no", ob.AltContactNo);
                cmd.Parameters.AddWithValue("@email", ob.Email);
                cmd.Parameters.AddWithValue("@category", ob.Category);
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return 1;
                    //not inseretd successfully
                }
                else
                {
                    return 2;
                    //return "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }


        }
        public int insert_AddService(AddLabService ob)
        {

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddService";
                cmd.Connection = con;
                // cmd.Parameters.AddWithValue("@LabId", "0");
                cmd.Parameters.AddWithValue("@LabId", ob.LabId);
                cmd.Parameters.AddWithValue("@TestName", ob.TestName);

                cmd.Parameters.AddWithValue("@TestDescription", ob.TestDescription);
                cmd.Parameters.AddWithValue("@TestDuration ", ob.TestDuration);
                cmd.Parameters.AddWithValue("@CostOfTest ", ob.CostOfTest);
                cmd.Parameters.AddWithValue("@LabHome", ob.LabHome);

                cmd.Parameters.Add("@TestCode", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //   cmd.Parameters["@LabId"].Direction = ParameterDirection.Output;
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return 1;
                    //not inseretd successfully
                }
                else
                {

                    return 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        public int Update_AddService(AddLabService ob)
        {

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_updateService";
                cmd.Connection = con;
                // cmd.Parameters.AddWithValue("@LabId", "0");
                cmd.Parameters.AddWithValue("@TestCode", ob.TestCode);
                cmd.Parameters.AddWithValue("@TestName", ob.TestName);

                cmd.Parameters.AddWithValue("@TestDescription", ob.TestDescription);
                cmd.Parameters.AddWithValue("@TestDuration ", ob.TestDuration);
                cmd.Parameters.AddWithValue("@CostOfTest ", ob.CostOfTest);
                cmd.Parameters.AddWithValue("@LabHome", ob.LabHome);
                //   cmd.Parameters["@LabId"].Direction = ParameterDirection.Output;
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return 1;
                    //not inseretd successfully
                }
                else
                {

                    return 2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        public AddLabService GetTests(int LabId)
        {
            SqlConnection con = null;
            AddLabService objects = new AddLabService();
            int count = 0;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetTest";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@LabId", LabId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    objects.TestName = reader["TestName"].ToString();
                    objects.TestCode = reader["TestCode"].ToString();
                    objects.StartTime= Convert.ToDateTime(reader["StartTime"]);
                    objects.Date = Convert.ToDateTime(reader["Date"]);
                    count++;
                }
                if (count == 0)
                {
                    objects.LabId = "1";
                }
                return objects;
            }
            catch (Exception ex)
            {
                return objects;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
