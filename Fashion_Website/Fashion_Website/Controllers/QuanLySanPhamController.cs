using Fashion_Website.Models.mapSanPham;
using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models.taoMa;

namespace Fashion_Website.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        public ActionResult Index()
        {
            return View();
        }

        //Hiển thị danh sách sản phẩm
        public ActionResult DanhSachSanPham()
        {
            fashionDBEntities db = new fashionDBEntities();
            var danhsachsp = db.SANPHAMs.ToList();
            return View(danhsachsp);
        }

        //Xử lý thêm sản phẩm
        public ActionResult ThemSanPham()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemSanPham(SANPHAM model)
        {
            mapSanPham map = new mapSanPham();
            if (map.ThemMoiSP(model) == true)
            {
                return RedirectToAction("DanhSachSanPham");
            }
            else
            {
                return View(model);
            }
        }

        //Xử lý cập nhật sản phẩm
        public ActionResult CapNhatSanPham(String MaSP)
        {
            var map = new mapSanPham();
            var sanPhamEdit = map.ChiTietSanPham(MaSP);
            return View(sanPhamEdit);
        }

        [HttpPost]
        public ActionResult CapNhatSanPham(SANPHAM model)
        {
            mapSanPham map = new mapSanPham();
            if (map.UpdateSP(model) == true)
            {
                return RedirectToAction("DanhSachSanPham");
            }
            else
            {
                return View(model);
            }
        }

        //Xử lý xóa sản phẩm
        public ActionResult XoaSanPham(String MaSP)
        {
            mapSanPham map = new mapSanPham();
            map.DeleteSP(MaSP);
            return RedirectToAction("DanhSachSanPham");
        }
    }
}