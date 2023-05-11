using Fashion_Website.Models;
using Fashion_Website.Models.mapSanPham;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult TrangChu()
        {
            return View();
        }      

        public ActionResult SanPham()
        {
            fashionDBEntities db = new fashionDBEntities();
            var danhSachSP = db.SANPHAMs.ToList();
            return View(danhSachSP);
        }
        public ActionResult ChinhSach()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult HeThongCuaHang()
        {
            return View();
        }
    }
}