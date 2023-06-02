using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Fashion_Website.Models;
using PagedList;

namespace Fashion_Website.Controllers
{
    public class KhachHangController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: KhachHang
        public ActionResult Index(string currentFilter, string search, int? page)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            int pageSize = 10;
            int pageNum = (page ?? 1);

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var customers = from l in db.KHACHHANGs
                            select l;

            if (search != null)
            {
                customers = db.KHACHHANGs.Where(customer => customer.MaKH.Contains(search) ||
                customer.HoTen.Contains(search) ||
                customer.SDT.Contains(search) ||
                customer.Email.Contains(search) ||
                customer.DiaChi.Contains(search));
            }
            customers = customers.OrderBy(id => id.MaKH);
            return View(customers.ToPagedList(pageNum, pageSize));
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: KhachHang/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,HoTen,SDT,Email,NgaySinh,GioiTinh,DiaChi,Username,UserPass,TinhTrang,avatar")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACHHANGs.Add(kHACHHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHACHHANG);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "MaKH,HoTen,SDT,Email,NgaySinh,GioiTinh,DiaChi,Username,UserPass,TinhTrang,avatar")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: KhachHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
