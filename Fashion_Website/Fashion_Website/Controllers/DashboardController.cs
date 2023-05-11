using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class DashboardController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.countSanPham = db.SANPHAMs.Count();
            ViewBag.countLoaiSanPham = db.LOAISANPHAMs.Count();
            ViewBag.countKhachHang = db.KHACHHANGs.Count();
            ViewBag.countDoanhThu = db.HOADONs.Count();
            return View();
        }
    }
}