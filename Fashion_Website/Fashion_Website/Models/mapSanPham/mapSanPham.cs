using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.mapSanPham
{
    public class mapSanPham
    {
        public SANPHAM ChiTietSanPham(String masp)
        {
            fashionDBEntities db = new fashionDBEntities();
            return db.SANPHAMs.SingleOrDefault(m => m.MaSP == masp);
        }
    }
}