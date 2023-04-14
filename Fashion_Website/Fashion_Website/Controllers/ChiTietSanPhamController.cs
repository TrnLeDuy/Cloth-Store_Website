using Fashion_Website.Models.mapSanPham;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: ChiTietSanPham
        public ActionResult ChiTietSP(String masp)
        {
            var map = new mapSanPham();
            var chiTietSP = map.ChiTietSanPham(masp);
            return View(chiTietSP); 
        }
    }
}