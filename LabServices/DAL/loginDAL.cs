using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using LabServices.Models;
using System.Web.Configuration;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
namespace LabServices.DAL
{
    public class loginDAL
    {

        public DataSet GetLSP()
        {
            SqlConnection con = null;
            DataSet ds = new DataSet();
         //   PatientModel objects = new PatientModel();
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

        //verify user
        public string VerifyUser(string userid)
        {
        
            int count = 0;
            SqlConnection con = null;         
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "VerifyUser";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@userid", userid);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt16(reader["counts"]);
                
                }
                if (count == 0)
                {
                    // 1 means no user exits with this id
                    return "0";
                }
                else
                {
                    // user exits
                    return userid;
                }
            }
            catch (Exception ex)
            {
                //some error occured
                return "0";
            }
            finally
            {
                con.Close();
            }


        }
        //check user login details 
        public int Login(LoginModel ob)
        {
            SqlConnection con = null;
            int count = 0;
            string password = null;
            string type = null;
           
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "FetchPassword";
                cmd.CommandType = CommandType.StoredProcedure;
               
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@userID", ob.UserID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Password"].ToString();
                    type = reader["UserType"].ToString();
                 
                    count++;
                }
                if (count == 0)
                { 
                    // 1 means no user exits with this id
                    return 1;
                }
                 else
                {
                    if (password == ob.Password)
                    {   
                        //if password verified
                        return 2;
                    }
                    else
                    {
                        return 3; 
                        //if password is not valid
                    }
                }
            }
            catch (Exception ex)
            {  
                //some error occured
                return 4;    
            }
            finally
            {
                con.Close();
            }
          
        }
        public int ChangePassword(LoginModel ob)
        {
            SqlConnection con = null;
            int count = 0;
            // string password = null;
      
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ChangePassword";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserId", ob.UserID);
                cmd.Parameters.AddWithValue("@Password", ob.Password);
                cmd.Parameters.AddWithValue("@NewPassword", ob.NewPassword);

                count = cmd.ExecuteNonQuery();
            
                if (count>0)
                {
                    //password changed succhessfully
                    return 1;

                }
           
                else
                    // invalid cuerrent password
                    return 0;
            }
            catch (Exception ex)
            {
                //some error occured
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        public string GetLoginType(string userid)
        {
            SqlConnection con = null;
            string type = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "GetUserType";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;        
                cmd.Parameters.AddWithValue("@userid",userid);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    type = (reader["UserType"]).ToString();

                }
                return type;
            }
            catch (Exception ex)
            {
                //some error occured
                return "0";
            }
            finally
            {
                con.Close();
            }
        }

        public string VerifyQue(LoginModel ob)
        {
            SqlConnection con = null;
            string count = null;
            // string password = null;
            string userid = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "verifyque";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@FavFood", ob.FavFood);
                cmd.Parameters.AddWithValue("@FavActor", ob.FavActor);
                cmd.Parameters.AddWithValue("@BirthCity", ob.BirthCity);
                cmd.Parameters.AddWithValue("@UserID", ob.UserID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    count = reader["password"].ToString();
                  
                }
                if (count == null)
                { 
                    //security question doesnot match
                    return null;
                }
                else
                    // securtiy question matched
                    return count;
            }
            catch (Exception ex)
            {
                //some error occured
                return null;
            }
            finally
            {
                con.Close();
            }
        }
    }
} 