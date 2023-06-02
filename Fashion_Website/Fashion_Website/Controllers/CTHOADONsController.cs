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
    public class CTHOADONsController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: CTHOADONs
        public ActionResult Index()
        {
            var cTHOADONs = db.CTHOADONs.Include(c => c.HOADON).Include(c => c.SANPHAM);
            return View(cTHOADONs.ToList());
        }

        // GET: CTHOADONs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHOADON cTHOADON = db.CTHOADONs.Find(id);
            if (cTHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cTHOADON);
        }

        // GET: CTHOADONs/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }

        // POST: CTHOADONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MACTHD,TenSP,DonGia,SoLuong,KichCo,ThanhTien,MaHD,MaSP")] CTHOADON cTHOADON)
        {
            if (ModelState.IsValid)
            {
                db.CTHOADONs.Add(cTHOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cTHOADON.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cTHOADON.MaSP);
            return View(cTHOADON);
        }

        // GET: CTHOADONs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHOADON cTHOADON = db.CTHOADONs.Find(id);
            if (cTHOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cTHOADON.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cTHOADON.MaSP);
            return View(cTHOADON);
        }

        // POST: CTHOADONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MACTHD,TenSP,DonGia,SoLuong,KichCo,ThanhTien,MaHD,MaSP")] CTHOADON cTHOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTHOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cTHOADON.MaHD);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cTHOADON.MaSP);
            return View(cTHOADON);
        }

        // GET: CTHOADONs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTHOADON cTHOADON = db.CTHOADONs.Find(id);
            if (cTHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cTHOADON);
        }

        // POST: CTHOADONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CTHOADON cTHOADON = db.CTHOADONs.Find(id);
            db.CTHOADONs.Remove(cTHOADON);
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
