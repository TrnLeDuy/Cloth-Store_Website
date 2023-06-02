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
    public class NhanVienController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: NhanVien
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

            var employees = from l in db.ADMINs
                            select l;
            
            if (!String.IsNullOrEmpty(search))
            {
                employees = db.ADMINs.Where(emp => emp.SDT.Contains(search) ||
                emp.MaAD.Contains(search) ||
                emp.Email.Contains(search) ||
                emp.HoTen.Contains(search));
            }
            employees = employees.OrderBy(id => id.MaAD);

            return View(employees.ToPagedList(pageNum, pageSize));
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaAD,HoTen,SDT,Email,NgaySinh,GioiTinh,DiaChi,Username,UserPass,TinhTrang,ChucVu,avatar")] ADMIN aDMIN)
        {
            if (ModelState.IsValid)
            {
                db.ADMINs.Add(aDMIN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDMIN);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaAD,HoTen,SDT,Email,NgaySinh,GioiTinh,DiaChi,Username,UserPass,TinhTrang,ChucVu,avatar")] ADMIN aDMIN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDMIN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDMIN);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["Role"] == null)
                return RedirectToAction("Login", "Authencation");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADMIN aDMIN = db.ADMINs.Find(id);
            if (aDMIN == null)
            {
                return HttpNotFound();
            }
            return View(aDMIN);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ADMIN aDMIN = db.ADMINs.Find(id);
            db.ADMINs.Remove(aDMIN);
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
