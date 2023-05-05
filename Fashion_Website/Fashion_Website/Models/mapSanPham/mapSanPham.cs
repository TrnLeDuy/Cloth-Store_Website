using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.mapSanPham
{
    public class mapSanPham
    {

        //lấy ra danh sách mã loại sản phẩm
        public List<LOAISANPHAM> DanhSachLoaiSP()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from Loai in db.LOAISANPHAMs
                        select Loai).ToList();  
            return data;    
        }

        //Lấy ra chi tiết sản phẩm
        public SANPHAM ChiTietSanPham(String MaSP)
        {
            fashionDBEntities db = new fashionDBEntities();
            return db.SANPHAMs.SingleOrDefault(m => m.MaSP == MaSP);
        }

        //Danh sách sản phẩm
        public List<SANPHAM> DanhSachSanPham()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from SanPham in db.SANPHAMs
                        select SanPham).ToList();
            return data;
        }

        //Thêm mới sản phẩm
        public bool ThemMoiSP(SANPHAM model)
        {
            try
            {
                fashionDBEntities db = new fashionDBEntities();
                db.SANPHAMs.Add(model);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Cập nhật sản phẩm
        public bool UpdateSP(SANPHAM model)
        {
            fashionDBEntities db = new fashionDBEntities();
            var updateModel = db.SANPHAMs.Find(model.MaSP);

            if (updateModel == null)
            {
                return false;
            }

            //Cập nhật giá trị
            updateModel.TenSP = model.TenSP;
            updateModel.MaLoaiSP = model.MaLoaiSP;
            updateModel.HinhSP = model.HinhSP;
            updateModel.MoTa = model.MoTa;
            updateModel.GiaSP = model.GiaSP;
            updateModel.TinhTrangSP = model.TinhTrangSP;
            db.SaveChanges();
            return true;
        }

        //Xóa sản phẩm
        public bool DeleteSP(String MaSP)
        {
            fashionDBEntities db = new fashionDBEntities();
            var model = db.SANPHAMs.Find(MaSP);

            if (model != null)
            {
                db.SANPHAMs.Remove(model);
                db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}