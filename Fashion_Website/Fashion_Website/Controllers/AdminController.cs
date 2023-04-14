using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin View
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult QLTK()
        {
            return View();
        }

        public ActionResult QLNTD()
        {
            return View();
        }

        public ActionResult QLSP()
        {
            return View(); 
        }

        public ActionResult QLDH()
        {
            return View();
        }

        public ActionResult QLHD()
        {
            return View();
        }

    }
}