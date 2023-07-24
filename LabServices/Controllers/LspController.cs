using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabServices.DAL;
using LabServices.Models;
using System.Data;
using System.Data.SqlClient;

namespace LabServices.Controllers
{
    public class LspController : Controller
    {
        // GET: Lsp
        
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("ManageProfile", "Lsp");
            else
                return RedirectToAction("Index", "Login");
        }

        public ActionResult ManageProfile()

        {
            LSPDal obj = new LSPDal();
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");
            else
            {
                string userid = Session["UserId"].ToString();

                DataSet ds = obj.GetData(userid);
                LSPModel ob = new LSPModel();
                ob.LabId = userid;
                try
                {
                    ob.Address = ds.Tables[0].Rows[0]["Address1"].ToString();
                    ob.Name = ds.Tables[0].Rows[0]["name"].ToString();
                    ob.City = ds.Tables[0].Rows[0]["city"].ToString();
                    ob.State = ds.Tables[0].Rows[0]["state1"].ToString();
                    ob.ZipCode = Convert.ToInt32(ds.Tables[0].Rows[0]["zipcode"]);
                    ob.Email = ds.Tables[0].Rows[0]["email"].ToString();
                    ob.Category = ds.Tables[0].Rows[0]["category"].ToString();
                    ob.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["contact_no"]);
                    ob.AltContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["alternate_no"]);
                    ob.LabId = userid;
                    Session["Cateogry"] = ob.Category; return View("LSPUpdate", ob);
                }
                catch (Exception ex)
                {
                    ViewData["errormsg"] = ex;
                    return View("LSPUpdate", ob);
                }
            }
        }
        public ActionResult EditOtherLsp(string userid)
        {
            LSPDal obj = new LSPDal();
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");
            else
            {
             

                DataSet ds = obj.GetData(userid);
                LSPModel ob = new LSPModel();
                try
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        ob.Address = ds.Tables[0].Rows[0]["Address1"].ToString();
                        ob.Name = ds.Tables[0].Rows[0]["name"].ToString();
                        ob.City = ds.Tables[0].Rows[0]["city"].ToString();
                        ob.State = ds.Tables[0].Rows[0]["state1"].ToString();
                        ob.ZipCode = Convert.ToInt32(ds.Tables[0].Rows[0]["zipcode"]);
                        ob.Email = ds.Tables[0].Rows[0]["email"].ToString();
                        ob.Category = ds.Tables[0].Rows[0]["category"].ToString();
                        ob.ContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["contact_no"]);
                        ob.AltContactNo = Convert.ToInt64(ds.Tables[0].Rows[0]["alternate_no"]);
                        ob.LabId = userid;
                    }
                }
                catch(Exception ex)
                {
                    ViewData["errormsg"] = "Try Again !!!";
                    return View("LSPUpdate", ob);

                }
                return View("LSPUpdate", ob);
            }
        }
        public ActionResult ViewAnotherLsp()
        {
            LSPDal obj = new LSPDal();
            if (Session["UserId"] != null)
            {
                string userid = Session["UserId"].ToString();
                DataSet ds = obj.GetAnotherLSP(userid);
                LSPModel ob = new LSPModel();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    ViewData["successmsg"] = "No Record Found!!!";
                    return View("Notification");
                }
                else
                {
                    ob.Data = ds;
                    return View("ViewLSP", ob);
                  
                }
            }
            else
                return RedirectToAction("Index", "Login");
        
        
          
        }
        public ActionResult ViewLabServices()
        {
            LSPDal obj = new LSPDal();
            if (Session["UserId"] != null)
            {
                AddLabService ob = new AddLabService();
                string userid = Session["UserId"].ToString();
                DataSet ds = obj.GetServices(userid);
                try {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ob.Data = ds;

                    }
                    else
                    {
                       
                        ViewData["successmsg"] ="No Record Found!!!" ;


                        return View("Notification");
                    }
                }
                catch(Exception ex)
                {
                    ob.Data = ds;
                    ViewData["errormsg"] = "Try Again !!!";
                    return View("Notification");
                }
                return View("Services", ob);
            }
            else
                return RedirectToAction("Index", "Login");



        }

        public ActionResult DeleteLSP(string userid)
        {
            LSPDal ob = new LSPDal();
            int status = ob.DeleteLsp(userid);
            if (status == 0)
            {
                return View("ViewLSP");
            }
            else
            {
                return RedirectToAction("ViewAnotherLsp", "Lsp");
            }
        }
        public ActionResult LspRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LspRegister(LSPModel obj)
        {
            LSPDal ob = new LSPDal();
            obj.Passsword = RandomPassword.Generate(9, 10);
            if (Session["UserId"] != null)
            {

                obj.ParentId = Session["UserId"].ToString();
                string Labid = ob.insert_LSP(obj);
                if (Labid == "1" || Labid == "0")
                {
                    string msg = "Try Again";
                    ViewData["Msg"] = msg;
                }
                else
                {
                    string msg = "Registered Successfully!!!";
                    ViewData["Msg"] = msg;
                    ViewData["LabId"] = Labid;
                    ViewData["Password"] = obj.Passsword;
                  
                }

            }
            return View();

        }

        //update lsp
        public ActionResult UpdateData(LSPModel obj)
        {

            LSPDal ob = new LSPDal();
            string LabId = obj.LabId;
            int count = 0;
            long contact = obj.ContactNo;
            while (contact != 0)
            {

                contact = contact / 10; count++;
            }
            if (count < 10 || count > 10)
            {
                ViewData["errormsg"] = "Invalid Contact";
                return View("LSPUpdate", obj);
            }
            count = 0;
            long acontact = obj.AltContactNo;
            while (acontact != 0)
            {

                acontact = acontact / 10; count++;
            }
            if (count < 10 || count > 10)
            {
                ViewData["errormsg"] = "Invalid Contact";
                return View("LSPUpdate", obj);
            }

            int i = ob.UpdateDAL(obj);
           
            if (i == 1)
            {
                ViewData["errormsg"] = "Try Again !!!";
             }
            else
            {
                ViewData["successmsg"] = "Updated Successfully !!!";
            }
            ModelState.Clear();
            obj.LabId = LabId;
            Session["Category"] = obj.Category;
            if (Session["UserId"] != null)
            {
                if (LabId != Session["UserId"].ToString())
                {
                    return RedirectToAction("ViewAnotherLsp", "Lsp");
                }
            }
                return View("LSPUpdate",obj);
        }

        //get service data from id
        public ActionResult EditServices(string testcode)
        {
            LSPDal obj = new LSPDal();
            if (Session["UserId"] == null)
                return RedirectToAction("Index", "Login");
            else
            {


                DataSet ds = obj.GetServiceId(testcode);
                AddLabService ob = new AddLabService();
                try {
                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        ob.TestName = ds.Tables[0].Rows[0]["TestName"].ToString();
                        ob.TestDescription = ds.Tables[0].Rows[0]["TestDescription"].ToString();
                        ob.LabHome = ds.Tables[0].Rows[0]["LabHome"].ToString();
                        ob.TestDuration = Convert.ToInt32(ds.Tables[0].Rows[0]["TestDuration"]);
                        ob.CostOfTest = Convert.ToDouble(ds.Tables[0].Rows[0]["CostOfTest"]);
                        ob.LabId = ds.Tables[0].Rows[0]["LabId"].ToString();

                    }
                }
                catch(Exception ex)

                {
                    ViewData["errormsg"] = "Try Again !!!";
                    return View("EditServices", ob);
                }
                return View("EditServices", ob);
            }
        }
        public ActionResult AddLabServices()
        {
            AddLabService ob = new AddLabService();
            if (Session["UserId"] != null)
            {
                ob.LabId = Session["UserId"].ToString();
                return View("AddLabServices", ob);
            }
            else

            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult AddLabServices(AddLabService obj)
        {
            
                LSPDal ob = new LSPDal();
                obj.LabId = Session["UserId"].ToString();
                int i = ob.insert_AddService(obj);
                if (i == 1)
                {
                    ViewData["errormsg"] = "Please Try Again !!!";
                }
                else
                {
                    ViewData["successmsg"] = "Added Successfully !!!";
                }
           
            obj.LabId = Session["UserId"].ToString();
            return View("AddLabServices",obj);
        }

        /*   public ActionResult UpdateLabService()
           {
               LSPDal obj = new LSPDal();
               string userid = Session["UserId"].ToString();

               DataSet ds = obj.GetData(userid);
               AddLabService ob = new AddLabService();
               ob.TestName = ds.Tables[0].Rows[0]["TestName "].ToString();
               ob.TestDescription= ds.Tables[0].Rows[0]["TestDescription"].ToString();
               ob.TestDuration = Convert.ToInt16(ds.Tables[0].Rows[0]["TestDuration "]);
               ob.CostOfTest = Convert.ToDouble(ds.Tables[0].Rows[0]["CostOfTest"]);
               ob.LabHome = ds.Tables[0].Rows[0]["city"].ToString();

               ob.LabId = userid;
               return View("UpdateLabService", ob);
           }*/
        public ActionResult DeleteServices(string testcode)
        {
            LSPDal ob = new LSPDal();
            int status = ob.DeleteService(testcode);
            if (status == 0)
            {
                return RedirectToAction("ViewLabServices", "Lsp");
            }
            else
            {
                return RedirectToAction("ViewLabServices", "Lsp");
            }
        }
        public ActionResult UpdateService(AddLabService obj)

        {
           LSPDal ob = new LSPDal();
            obj.LabId = Session["UserId"].ToString();
            int i = ob.Update_AddService(obj);
            if (i == 1)
            {
                ViewData["errormsg"] = "Try Again !!!";
            }
            else
            {
                ViewData["successmsg"] = "Updated Successfully !!!";
                return RedirectToAction("ViewLabServices", "Lsp");
            }

            return View("EditServices",obj);
        }

        public ActionResult AddLabSchedule()
        {
            ViewData["message"] = null;
            return View();
        }
        public ActionResult SelectTestSchedule()
        {

            return View();
        }

        public ActionResult TestForSchedule(string testcode)
        {
            LSPDal obj = new LSPDal();
            //if (Session["UserId"] == null)
            //    return RedirectToAction("Index", "Login");
            //else
            //{


                DataSet ds = obj.GetServiceId(testcode);
                AddLabService ob = new AddLabService();
                if (ds.Tables[0].Rows.Count != 0)
                {

                    ob.TestName = ds.Tables[0].Rows[0]["TestName"].ToString();                   
                    ob.TestCode = ds.Tables[0].Rows[0]["TestCode"].ToString();
                     ob.LabId = ds.Tables[0].Rows[0]["LabId"].ToString();
            }
                return View("SelectTestSchedule",ob);
            //}
            
        }



        public ActionResult FetchTest(AddLabService ob)
        {
            String Labid = ob.LabId;
            LSPDal obj = new LSPDal();
            ob.Data = obj.GetServiceSchedule(Labid);
            ViewData["message"] = "Yes";
            return View("AddLabSchedule", ob);
        }

    }

}
