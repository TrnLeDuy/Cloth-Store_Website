using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}