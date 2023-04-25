using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        [HttpGet]
        public ActionResult GioHang()
        {
            fashionDBEntities db = new fashionDBEntities();
            var giohang = db.GioHangs.ToList();
            return View(giohang);
        }
        [HttpPost]
        public ActionResult GioHang(GioHang model)
        {
            fashionDBEntities db = new fashionDBEntities();
            GioHang giohang = new GioHang();
            giohang = model;
            db.GioHangs.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}