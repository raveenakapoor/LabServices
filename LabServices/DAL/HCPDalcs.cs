using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using LabServices.Models;
using System.Configuration;

namespace LabServices.DAL
{
    public class HCPDalcs
    {

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
        public string insert_HCP(HCPModel ob)
        {

            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Register_HCP";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@name", ob.Name);
                cmd.Parameters.AddWithValue("@license_no", ob.License_no);
                cmd.Parameters.AddWithValue("@Address1", ob.Address);
                cmd.Parameters.AddWithValue("@zipcode", ob.ZipCode);
                cmd.Parameters.AddWithValue("@city", ob.City);
                cmd.Parameters.AddWithValue("@state1", ob.State);
                cmd.Parameters.AddWithValue("@contact_no", ob.ContactNo);
                cmd.Parameters.AddWithValue("@alternate_no", ob.AltContactNo);
                cmd.Parameters.AddWithValue("@email", ob.Email);
          
                cmd.Parameters.AddWithValue("@FavActor", ob.Answer1);
                cmd.Parameters.AddWithValue("@FavFood", ob.Answer2);
                cmd.Parameters.AddWithValue("@BirthCity", ob.Answer2);
                cmd.Parameters.AddWithValue("@Password", ob.Passsword);
                cmd.Parameters.AddWithValue("@UserType", "HCP");
                cmd.Parameters.Add("@HCPId", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //   cmd.Parameters["@LabId"].Direction = ParameterDirection.Output;
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return "1";
                    //not inseretd successfully
                }
                else
                {
                    string labid = (cmd.Parameters["@HCPId"].Value).ToString();
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

        public int update_Patient(HCPModel ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Update_HCP";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", ob.HCPId);
                cmd.Parameters.AddWithValue("@name", ob.Name);
                cmd.Parameters.AddWithValue("@license_no", ob.License_no);
                cmd.Parameters.AddWithValue("@Address1", ob.Address);
                cmd.Parameters.AddWithValue("@state1", ob.State);
                cmd.Parameters.AddWithValue("@city", ob.City);
                cmd.Parameters.AddWithValue("@zipcode", ob.ZipCode);
                cmd.Parameters.AddWithValue("@email", ob.Email);
                cmd.Parameters.AddWithValue("@contact_no", ob.ContactNo);
                cmd.Parameters.AddWithValue("@alternate_no", ob.AltContactNo);
                
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return 1;
                    //not inseretd successfully
                }
                else
                {
                    return 2;
                    //return Convert.ToInt32(cmd.Parameters["@userid"].Value);
                    //inserted successfully
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


        public DataSet HCPPatient(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            PatientModel objects = new PatientModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "HCP_Patient";
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
        public DataSet GetLSP()
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            PatientModel objects = new PatientModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllLSP";
                cmd.Connection = con;

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

        public DataSet GetData(string UserId)
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
            HCPModel objects = new HCPModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetHCPData";
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

    }

}

