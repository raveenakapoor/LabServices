using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabServices.Models;
using LabServices.DAL;
using System.Data.SqlClient;
using System.Data;

namespace LabServices.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult LspProviders()
        {
            HCPDalcs obj = new HCPDalcs();
           

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
          
        
        // load change password view
        public ActionResult Login()
        {
            return View("Login");
        }
        
        public ActionResult ChangePassword()
        {
            return View();
        }
        // change password
        public JsonResult NewPassword(LoginModel ob)
        {
            loginDAL obj = new loginDAL();

            ob.UserID = Session["UserId"].ToString();
            int result = obj.ChangePassword(ob);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ForgotPassword()
        {
            return View("ForgetPassword");
        }
        public ActionResult VerifyQuestion(LoginModel obj)
        {
            loginDAL ob = new loginDAL();
            string result = ob.VerifyQue(obj);
            string msg = null;
            if(result==null)
            {
                msg = "Question Not Verified";
                ViewData["msg"] = msg;
            }
            else 
            {
                ViewData["Password"]=result;
            }
                //redired to change password
            return View("ForgetPassword");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
        public JsonResult VerifyId(string userid)
        {

            loginDAL obj = new loginDAL();
            string result = obj.VerifyUser(userid);  
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        // GET: Login
       
        public ActionResult Index()
        {
            //this is a comment
            return View();
        }
        public ActionResult CheckLogin(LoginModel ob)
        {
            
                loginDAL obj = new loginDAL();
                int result = obj.Login(ob);
                string msg = null;
            Session["Category"] = null;
            Session["UserType"] = null;
            if (result == 1)
            {
                // no user found with id
                msg = "No User Found";
            }
            else if (result == 2)
            {
                // string type = obj.GetLoginType(ob.UserID);
                string type = ob.UserID.Substring(0,3);
                Session["UserId"] = ob.UserID;
                Session["UserType"] = type;
                if (type == "LAB")
                    return RedirectToAction("Index", "Lsp");
                else if (type =="PAT")
                    return RedirectToAction("Index", "Patient");
                else
                    return RedirectToAction("Index", "HCP");
            }
            
                else if (result == 3)
                {
                    msg = "Invalid Password!!!";
                }
                else if (result == 4)
                {
                    msg = "Please Try Again!!!";
                }
                ViewData["Msg"] = msg;
            return View("Index");
        }
         
        public ActionResult ForgetPassword(LoginModel ob)
        {
            if (ModelState.IsValid)
            {
                loginDAL obj = new loginDAL();
        int result = obj.Login(ob);
        string msg = null;
                if (result == 1)
                {
                    // no user found with id
                    msg = "No User Found";
                }
                else if (result == 2)
                {
                    RedirectToAction("Index", "Dashboard");
}
                else if (result == 3)
                {
                    msg = "Invalid Password!!!";
                }
                else if (result == 4)
                {
                    msg = "Please Try Again!!!";
                }
                ViewData["Msg"] = msg;

               

            }
           
                return View();}
    }
}