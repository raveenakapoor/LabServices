using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabServices.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}