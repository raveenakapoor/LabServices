using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabServices.Models;
using LabServices.DAL;
using System.Data;

namespace LabServices.Controllers
{
    public class HCPController : Controller
    {
        // GET: UpdateHCP
        public ActionResult LspProviders()
        {
            HCPDalcs obj = new HCPDalcs();
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
                return RedirectToAction("UpdateHCP", "HCP");
            else
                return RedirectToAction("Index", "Login");
        }
      
        public ActionResult RegisterPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterPatient(PatientModel obj)
        {
            //if(ModelState.IsValid)
            //{
            try
            {


                PatientDal ob = new PatientDal();
                obj.Passsword = RandomPassword.Generate(9, 10);
                if (Session["UserId"] != null)
                {

                    obj.ParentId = Session["UserId"].ToString();

                }
                else
                    obj.ParentId = "0";
                string PatientId = ob.insert_Patient(obj);
                if (PatientId == "1" || PatientId == "0")
                {
                    string msg = "Try Again";
                    ViewData["Msg"] = msg;
                }
                else
                {
                    string msg = "Registered Successfully!!!";
                    ViewData["Msg"] = msg;
                    ViewData["LabId"] = PatientId;
                    ViewData["Password"] = obj.Passsword;
                }
            }
            catch (Exception ex)
            {

            }
            return View("RegisterPatient");
           // return View("RegisterPatient");
        }
        public ActionResult UpdateHCP()
        {
            HCPDalcs obj = new HCPDalcs();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                string userid = Session["UserId"].ToString();
                HCPModel ob = new HCPModel();
                ob.HCPId = userid;
                DataSet ds = obj.GetData(userid);
                try
                {

                    ob.Address = ds.Tables[0].Rows[0]["Address1"].ToString();
                    ob.Name = ds.Tables[0].Rows[0]["name"].ToString();
                    ob.License_no = ds.Tables[0].Rows[0]["license_no"].ToString();
                    ob.ZipCode = Convert.ToInt32(ds.Tables[0].Rows[0]["zipcode"]);
                    ob.City = ds.Tables[0].Rows[0]["city"].ToString();
                    ob.State = ds.Tables[0].Rows[0]["state1"].ToString();
                    ob.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["contact_no"]);
                    ob.AltContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["alternate_no"]);
                    ob.Email = ds.Tables[0].Rows[0]["email"].ToString();
                    ob.HCPId = userid;
                    return View("UpdateHCP", ob);
                }
                catch (Exception ex)
                {
                    return View("UpdateHCP", obj);
                }
            }
        }

        public ActionResult HCPUpdate(HCPModel obj)

        {
            HCPDalcs ob = new HCPDalcs();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                obj.HCPId = Session["UserId"].ToString();
                int i = ob.update_Patient(obj);
                if (i == 1)
                {
                    ViewData["errormsg"] = "Try Again !!!";
                }
                else
                {
                    ViewData["successmsg"] = "Updated Successfully !!!";
                }
            }

            return View("UpdateHCP",obj);
        }
        public ActionResult ViewHCPPatient()
        {
            HCPDalcs obj = new HCPDalcs();
            if (Session["UserId"] != null)
            {
                string userid = Session["UserId"].ToString();
                DataSet ds = obj.HCPPatient(userid);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ViewData["successmsg"] = "No record found";
                    return View("Notification");
                }
                else
                {
                    PatientModel ob = new PatientModel();
                    ob.Data = ds;
                    return View("ViewHCPPatient", ob);
                }
              
            }
            else
                return RedirectToAction("Index", "Login");



        }


        //:update hcp patient

        public ActionResult UpdateHCPPatient(string userid)
        {
            PatientDal obj = new PatientDal();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // string userid = Session["UserId"].ToString();
                try
                {
                    DataSet ds = obj.GetData(userid);
                    PatientModel ob = new PatientModel();
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
                    return View("UpdateHCPPatient", ob);

                }
                catch (Exception ex)
                {
                    ViewData["errormsg"] = "Try Again !!!";
                    return View("UpdateHCPPatient");
                }

            }
        }
        
        public ActionResult HCPPatientUpdate(PatientModel obj)

        {
            PatientDal ob = new PatientDal();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                obj.ParentId = Session["UserId"].ToString();
                int i = ob.update_HCPPatient(obj);
                if (i == 1)
                {
                    ViewData["errormsg"] = "Try Again !!!";
                }
                else
                {
                    ViewData["successmsg"] = "Updated Successfully !!!";
                }
            }
            return RedirectToAction("ViewHCPPatient");
        }


        public ActionResult DeleteHCP_patient(string userid)
        {
            PatientDal ob = new PatientDal();
            int status = ob.DeleteHCP_Patient(userid);
            if (status == 0)
            {
                return View("ViewHCPPatient");
            }
            else
            {
                return RedirectToAction("ViewHCPPatient", "HCP");
            }
        }
    }

    }




