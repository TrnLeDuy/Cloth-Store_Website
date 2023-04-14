using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.mapSanPham
{
    public class mapSanPham
    {
        public SanPham ChiTietSanPham(String masp)
        {
            fashionDBEntities db = new fashionDBEntities();
            return db.SanPhams.SingleOrDefault(m => m.MaSP == masp);
        }
    }
}