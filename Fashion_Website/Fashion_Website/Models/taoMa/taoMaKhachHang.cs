using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.taoMa
{
    public class taoMaKhachHang
    {
        public List<string> maKoGiaTri()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from KHACHHANG in db.KHACHHANGs
                        orderby KHACHHANG.MaKH ascending
                        select KHACHHANG.MaKH).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaKhachHang()
        {
            fashionDBEntities db = new fashionDBEntities();
            string macuoi = "";
            foreach (var item in new taoMaSanPham().maKoGiaTri())
            {
                macuoi = item.Substring(2, 3);
            }

            string ma1 = "KH";
            string s = "";

            if (db.KHACHHANGs.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "00"; }
                else if (k < 100)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }
    }
}