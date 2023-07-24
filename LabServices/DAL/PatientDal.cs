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
    public class PatientDal
    {
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

        public string insert_Patient(PatientModel ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Register_patient";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Parentid", ob.ParentId);
                cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                cmd.Parameters.AddWithValue("@Age", ob.Age);
                cmd.Parameters.AddWithValue("@Address", ob.Address);
                cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
                cmd.Parameters.AddWithValue("@MedicalComplaint", ob.MedicalComplaint);
                cmd.Parameters.AddWithValue("@ReferredDoctor", ob.ReferredDoctor);
                cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
                cmd.Parameters.AddWithValue("@Password", ob.Passsword);
                cmd.Parameters.AddWithValue("@UserType", "Patient");
                cmd.Parameters.AddWithValue("@FavActor", ob.Answer1);
                cmd.Parameters.AddWithValue("@FavFood", ob.Answer2);
                cmd.Parameters.AddWithValue("@BirthCity", ob.Answer2);
                cmd.Parameters.Add("@PatientId", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return "1";
                    //not inseretd successfully
                }
                else
                {
                    string labid = (cmd.Parameters["@PatientId"].Value).ToString();
                    return (cmd.Parameters["@PatientId"].Value).ToString();
                    //inserted successfully
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
        public int DeleteDependents(int Dependentid)
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
                cmd.CommandText = "DeleteDependent";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@Dependentid", Dependentid);
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
        
        public int update_Patient(PatientModel ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Update_Patient";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", ob.PatientId);
                cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                cmd.Parameters.AddWithValue("@Age", ob.Age);
                cmd.Parameters.AddWithValue("@Address", ob.Address);
                cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
                cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
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

        /*  public int Update_Dependent(AddDependent ob)
          {
              SqlConnection con = null;
              try
              {
                  con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                  con.Open();
                  SqlCommand cmd = new SqlCommand();
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.CommandText = "sp_update_dependent";
                  cmd.Connection = con;
                  cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                  cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                  cmd.Parameters.AddWithValue("@relation", ob.Realtionship);
                  cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                  cmd.Parameters.AddWithValue("@Age", ob.Age);
                  cmd.Parameters.AddWithValue("@Address", ob.Address);
                  cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
                  cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
                  cmd.Parameters.AddWithValue("@HealthinsuranceNo", ob.HealthInsuranceNo);
                  cmd.Parameters.AddWithValue("@Patientid", ob.PatientId);

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
          }*/


        public string AddDependent(AddDependent ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AddDependant";
                cmd.Connection = con;
                if (ob.HealthInsuranceNo == 0)
                    ob.InsuranceCover = "No";
                else
                    ob.InsuranceCover = "Yes";
                cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                cmd.Parameters.AddWithValue("@relation", ob.Realtionship);
                cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                cmd.Parameters.AddWithValue("@Age", ob.Age);
                cmd.Parameters.AddWithValue("@Address", ob.Address);
                cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
                cmd.Parameters.AddWithValue("@PatientId", ob.PatientId);
                cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
                cmd.Parameters.AddWithValue("@HealthinsuranceNo", ob.HealthInsuranceNo);

                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return "1";
                    //not inseretd successfully
                }
                else
                {
                    //string labid = (cmd.Parameters["@PatientId"].Value).ToString();
                    return "2";
                    //inserted successfully
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

        public string Update_Dependent(AddDependent ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_update_dependent";
                cmd.Connection = con;
                if (ob.HealthInsuranceNo == 0)
                    ob.InsuranceCover = "No";
                else
                    ob.InsuranceCover = "Yes";
                cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                cmd.Parameters.AddWithValue("@relation", ob.Realtionship);
                cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                cmd.Parameters.AddWithValue("@Age", ob.Age);
                cmd.Parameters.AddWithValue("@Address", ob.Address);
                cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
           
                cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
                cmd.Parameters.AddWithValue("@HealthinsuranceNo", ob.HealthInsuranceNo);
                cmd.Parameters.AddWithValue("@DependentId", ob.DepandantId);
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return "1";
                    //not inseretd successfully
                }
                else
                {
                    //string labid = (cmd.Parameters["@PatientId"].Value).ToString();
                    return "2";
                    //inserted successfully
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



        //get data for 
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
                cmd.CommandText = "GetPatientData";
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
        public DataSet ShowDependent(string PatientId)
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
                cmd.CommandText = "sp_show_dependent";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@PatientId", PatientId);
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
    

        public DataSet GetDependentId(int DependentId)
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
                cmd.CommandText = "sp_get_dependentid";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@DependentId", DependentId);
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


        public int update_HCPPatient(PatientModel ob)
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_Update_HCPPatient";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@UserID", ob.PatientId);
                cmd.Parameters.AddWithValue("@FirstName", ob.FirstName);
                cmd.Parameters.AddWithValue("@LastName", ob.LastName);
                cmd.Parameters.AddWithValue("@Gender", ob.Gender);
                cmd.Parameters.AddWithValue("@Age", ob.Age);
                cmd.Parameters.AddWithValue("@Address", ob.Address);
                cmd.Parameters.AddWithValue("@ContactNo", ob.ContactNo);
                cmd.Parameters.AddWithValue("@MedicalComplaint", ob.MedicalComplaint);
                cmd.Parameters.AddWithValue("@ReferredDoctor", ob.ReferredDoctor);
                cmd.Parameters.AddWithValue("@InsuranceCover", ob.InsuranceCover);
                int rowaffected = cmd.ExecuteNonQuery();
                if (rowaffected < 0)
                {
                    return 1;
                    //not updated successfully
                }
                else
                {
                    return 2;
                    //return Convert.ToInt32(cmd.Parameters["@userid"].Value);
                    //updated successfully
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

        public int DeleteHCP_Patient(string userid)
        {
            SqlConnection con = null;
            int result = 0;
            PatientModel objects = new PatientModel();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Delete_HCP_Patient";
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

    }
}

