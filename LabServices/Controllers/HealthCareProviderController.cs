using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LabServices.DAL;
using LabServices.Models;

namespace LabServices.Controllers
{
    public class HealthCareProviderController : Controller
    {
        // GET: HealthCareProvider
        public ActionResult HCPRegister()
        {
            return View("HCPRegister");
        }

    }
}