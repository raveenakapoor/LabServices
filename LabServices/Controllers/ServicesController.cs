using LabServices.DAL;
using LabServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabServices.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
  
        public ActionResult ShowLSPLabServices(AddLabService obj)
        {
           try
            {
                ServicesDal ob = new ServicesDal();
                // PatientModel obPatientModel = new PatientModel();
                // DataSet ds;
                //string PatientId = Session["UserId"].ToString();
                obj.Data = ob.ShowLabServiceTest();

                return View("ShowLSPLabServices", obj);
                // obj.PatientId = Session["UserId"].ToString();
                //string PatientId = ob.AddDependent(obj);
                //string userid = Session["PatientId"].ToString();
                // DataSet ds = obj.ShowDependent();

            }
            catch (Exception ex)
            {

            }
           return View("ShowLSPLabServices");
           // return View("Dependent");
        }
    }
    
}