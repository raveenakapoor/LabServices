using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using LabServices.DAL;
using LabServices.Models;
using System.Security.Cryptography;



namespace LabServices.Controllers
{
    public class PatientController : Controller
    {
        public ActionResult LspProviders()
        {
            PatientDal obj = new PatientDal();
            if (Session["UserId"] != null)
            {

                DataSet ds = obj.GetLSP();
                LSPModel ob = new LSPModel();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ViewData["successmsg"] = "No Record Found!!!";
                    return View("Notification");
                }
                else
                {
                    ob.Data = ds;
                    return View("LabServiceProvider", ob);

                }
            }
            else
                return RedirectToAction("Index", "Login");
        }
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("ManageProfile", "Patient");
            else
                return RedirectToAction("Index", "Login");
        } 
        public ActionResult ManageProfile()
        {
            PatientModel ob = new PatientModel();
            PatientDal obj = new PatientDal();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
            string userid = Session["UserId"].ToString();
            try
            {
            DataSet ds = obj.GetData(userid);
         
            ob.Address = ds.Tables[0].Rows[0]["Address"].ToString();
            ob.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
            ob.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
            ob.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
            ob.Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Age"]);
            ob.MedicalComplaint = ds.Tables[0].Rows[0]["MedicalComplaint"].ToString();
            ob.ReferredDoctor = ds.Tables[0].Rows[0]["ReferredDoctor"].ToString();
            ob.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["ContactNo"]);
            ob.InsuranceCover = Convert.ToInt32(ds.Tables[0].Rows[0]["InsuranceCover"]);
                ob.PatientId = userid;
         

        }
                catch (Exception ex)
            {

                    ViewData["errormsg"] = "Some Error Occured";
                    return View("Notification");
                }
                return View("Update", ob);
            }
        }

        // GET: Patient
        public ActionResult PatientRegister()
        {
            return View();
        }
        public ActionResult AddDependant()
        {
            return View();
        }
        public ActionResult DeleteDepenent(int DepandantId)
        {
            PatientDal ob = new PatientDal();
            int status = ob.DeleteDependents(DepandantId);
            if (status == 0)
            {
                return View("GetDependant");
            }
            else
            {
                return RedirectToAction("GetDependant");
            }


        }
        public ActionResult GetDependant(AddDependent obj)
        {
            try
            {
                PatientDal ob = new PatientDal();
                // PatientModel obPatientModel = new PatientModel();
                // DataSet ds; 
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    string PatientId = Session["UserId"].ToString();

                    obj.data = ob.ShowDependent(PatientId);
                    if (obj.data.Tables[0].Rows.Count == 0)
                    {

                        ViewData["successmsg"] = "No record found";
                        return View("Notification");
                    }
                    else
                    {
                        return View("Dependent", obj);

                    }
                    // obj.PatientId = Session["UserId"].ToString();
                    //string PatientId = ob.AddDependent(obj);
                    //string userid = Session["PatientId"].ToString();
                    // DataSet ds = obj.ShowDependent();
                }
            }
            catch (Exception ex)
            {
                ViewData["errormsg"] = "Some Error Occured";
                return View("Notification");
            }
            //return View("AddDependant");
        }
        

        public ActionResult UpdateDependant(int DepandantId)
        {
            PatientDal obj = new PatientDal();
            //string userid = Session["UserId"].ToString();
            AddDependent ob = new AddDependent();
            DataSet ds = obj.GetDependentId(DepandantId);
            if (ds.Tables[0].Rows.Count == 0)
            {
                ViewData["successmsg"] = "No record found";
                return View("Notification");
            
           }
            else
            {
                
                try
                {
                    ob.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    ob.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    ob.Realtionship = ds.Tables[0].Rows[0]["relation"].ToString();
                    ob.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                    ob.Age = Convert.ToInt32(ds.Tables[0].Rows[0]["Age"]);
                    ob.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ob.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["ContactNo"]);
                    // ob.InsuranceCover = ds.Tables[0].Rows[0]["state1"].ToString();
                    ob.HealthInsuranceNo = Convert.ToInt32(ds.Tables[0].Rows[0]["HealthinsuranceNo"]);
                    //ob.PatientId = ds.Tables[0].Rows[0]["PatientId"];
                }
                catch (Exception ex)
                {

                    ViewData["successmsg"] = "No record found";
                    return View("Notification");
                }
            }

                // ob.DepandantId = DepandantId;
                return View("UpdateDependent", ob);
            }
        

        public ActionResult DependentUpdate(AddDependent obj)

        {
            PatientDal ob = new PatientDal();
            if (obj.Age < 18)
            {
                ViewData["errormsg"] = "Age Greater Than 18";
                return View("UpdateDependent",obj);
            }
            int i = Convert.ToInt16( ob.Update_Dependent(obj));
            if (i == 1)
            {
                ViewData["errormsg"] = "Try Again !!!";
                return RedirectToAction("GetDependant");
            }
            else
            {
                ViewData["successmsg"] = "Updated Successfully !!!";
            }
            return RedirectToAction("GetDependant");
        }

        public ActionResult DependantAdd(AddDependent obj)
        {
            try
            {


                PatientDal ob = new PatientDal();
                // DataSet ds; 
                if (Session["UserId"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {  if(obj.Age<18)
                    {
                        ViewData["errormsg"] = "Invalid Age";
                        return View("AddDependant");
                    }
                    obj.PatientId = Session["UserId"].ToString();
                    string PatientId = ob.AddDependent(obj);
                    if (PatientId == "1" || PatientId == "0")
                    {
                        string msg = "Try Again";
                        ViewData["Msg"] = msg; return View("AddDependant");
                    }
                    else
                    {
                        string msg = "Failed!!!";
                        //  ViewData["errormsg"] = msg;
                        ViewData["LabId"] = PatientId;
                        ViewData["successmsg"] = "Added Successfully !!!";
                        //ViewData["Password"] = obj.Passsword;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Try Again";
                ViewData["Msg"] = msg;
                return View("AddDependant");
            
        }
            return RedirectToAction("GetDependant");
        }


        public ActionResult PatientUpdate(PatientModel ob)
        {

            try
            {

                int count = 0;
                PatientDal obj = new PatientDal();
                long contact = ob.ContactNo;
                while (contact != 0)
                {

                    contact = contact / 10; count++;
                }
                if (count < 10 || count > 10)
                {
                    ViewData["errormsg"] = "Invalid Contact";
                    return View("Update", ob);
                }
                string PatientId = ob.PatientId;
            int i = obj.update_Patient(ob);
            if (i == 1)
            {
                ViewData["errormsg"] = "Try Again !!!";
            }
            else
        {
                ViewData["successmsg"] = "Updated Successfully !!!";
            }
            ModelState.Clear();
            ob.PatientId = PatientId;
          //  if (PatientId != Session["UserId"].ToString())
          //  {
          //      return RedirectToAction("ViewAnotherLsp", "Lsp");
           // }
            }
            catch (Exception ex)
            {
                ViewData["errormsg"] = "Try Again !!!";
            }
            return View("Update", ob);
        }

        
        
    }
}