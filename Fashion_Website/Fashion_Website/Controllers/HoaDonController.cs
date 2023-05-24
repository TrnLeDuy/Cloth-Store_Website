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

        // GET: HoaDon
        public ActionResult Index()
        {
            var hOADONs = db.HOADONs.Include(h => h.ADMIN).Include(h => h.DONHANG).Include(h => h.KHACHHANG);

            return View(hOADONs.ToList());
        }

        // GET: HoaDon/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // GET: HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.MaAD = new SelectList(db.ADMINs, "MaAD", "HoTen");
            ViewBag.MaDH = new SelectList(db.DONHANGs, "MaDH", "PTThanhToan");
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "HoTen");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,NgayLap,TongTien,MaKH,MaDH,MaAD")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.HOADONs.Add(hOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaAD = new SelectList(db.ADMINs, "MaAD", "HoTen", hOADON.MaAD);
            ViewBag.MaDH = new SelectList(db.DONHANGs, "MaDH", "PTThanhToan", hOADON.MaDH);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "HoTen", hOADON.MaKH);
            return View(hOADON);
        }

        // GET: HoaDon/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaAD = new SelectList(db.ADMINs, "MaAD", "HoTen", hOADON.MaAD);
            ViewBag.MaDH = new SelectList(db.DONHANGs, "MaDH", "PTThanhToan", hOADON.MaDH);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "HoTen", hOADON.MaKH);
            return View(hOADON);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,NgayLap,TongTien,MaKH,MaDH,MaAD")] HOADON hOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaAD = new SelectList(db.ADMINs, "MaAD", "HoTen", hOADON.MaAD);
            ViewBag.MaDH = new SelectList(db.DONHANGs, "MaDH", "PTThanhToan", hOADON.MaDH);
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "HoTen", hOADON.MaKH);
            return View(hOADON);
        }

        // GET: HoaDon/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON hOADON = db.HOADONs.Find(id);
            if (hOADON == null)
            {
                return HttpNotFound();
            }
            return View(hOADON);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOADON hOADON = db.HOADONs.Find(id);
            db.HOADONs.Remove(hOADON);
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
