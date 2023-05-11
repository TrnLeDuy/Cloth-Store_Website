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
        private decimal total = 0;

        // GET: Dashboard
        public ActionResult Dashboard()
        {
            foreach (var entity in db.HOADONs)
            {
                total += entity.TongTien;
            }

            ViewBag.countSanPham = db.SANPHAMs.Count();
            ViewBag.countDonHang = db.HOADONs.Count();
            ViewBag.countKhachHang = db.KHACHHANGs.Count();
            ViewBag.countDoanhThu = total;
            return View();
        }
    }
}