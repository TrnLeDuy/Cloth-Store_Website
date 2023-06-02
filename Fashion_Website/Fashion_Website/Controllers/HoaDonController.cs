using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models;

namespace Fashion_Website.Controllers
{
    public class HoaDonController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: HOADONs
        public ActionResult Index()
        {
            var hOADONs = db.HOADONs.Include(h => h.ADMIN).Include(h => h.DONHANG).Include(h => h.KHACHHANG);
            return View(hOADONs.ToList());
        }

    }
}