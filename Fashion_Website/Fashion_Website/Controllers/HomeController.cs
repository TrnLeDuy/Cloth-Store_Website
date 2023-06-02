using Fashion_Website.Models;
using Fashion_Website.Models.mapSanPham;
using PagedList;
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

        public ActionResult SanPham(int? page)
        {
            fashionDBEntities db = new fashionDBEntities();
            int pageSize = 4; // number of products to display per page
            int pageNumber = (page ?? 1); // if no page is specified, default to page 1

            var products = db.SANPHAMs.OrderBy(p => p.MaSP); // replace db.SANPHAMs with your own data source
            var pagedProducts = products.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);
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