using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models;

namespace Fashion_Website.Controllers
{
    public class DonHangController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: DonHang
        public ActionResult Index()
        {
            var dONHANGs = db.DONHANGs.Include(d => d.KHACHHANG);
            return View(dONHANGs.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult XacNhanDon (String MaDH)
        {
            fashionDBEntities db = new fashionDBEntities();
            var donhang = db.DONHANGs.FirstOrDefault(ma => ma.MaDH == MaDH);
   

            string maAD = Session["ID"].ToString().Trim();

            HOADON modelHoaDon = new HOADON();
            modelHoaDon.MaHD = new Fashion_Website.Models.taoMa.taoMaHoaDon().TaoMaHoaDon();
            modelHoaDon.NgayLap = DateTime.Now;
            modelHoaDon.TongTien = donhang.TongTien;
            modelHoaDon.MaKH = donhang.MaKH;
            modelHoaDon.MaAD = maAD;
            modelHoaDon.MaDH = donhang.MaDH;
            try
            {
                // Lưu dữ liệu vào cơ sở dữ liệu
                db.HOADONs.Add(modelHoaDon);
                donhang.TrangThaiDH = 1;
                db.SaveChanges();
                Session["HoaDon"] = modelHoaDon;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var error in ex.EntityValidationErrors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }

            var ctdon = db.CTDONHANGs.Where(ma => ma.MaDH == donhang.MaDH).ToList();
            foreach (var ct in ctdon)
            {
                CTHOADON ctHoaDon = new CTHOADON();
                ctHoaDon.MACTHD = new Fashion_Website.Models.taoMa.taoMaCTHD().TaoMaCTHD();
                ctHoaDon.TenSP = ct.TenSP;
                ctHoaDon.DonGia = ct.DonGia;
                ctHoaDon.SoLuong = ct.SoLuongDat;
                ctHoaDon.KichCo = ct.KichCo;
                ctHoaDon.ThanhTien = ct.DonGia * ct.SoLuongDat;
                ctHoaDon.MaHD = modelHoaDon.MaHD;
                ctHoaDon.MaSP = ct.MaSP;
                try
                {
                    // Lưu dữ liệu vào cơ sở dữ liệu
                    db.CTHOADONs.Add(ctHoaDon);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in error.ValidationErrors)
                        {
                            Debug.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }
            }


            return RedirectToAction("Index", "DonHang");
        }

    }
}
