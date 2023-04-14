using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.taoMa
{
    public class taoMaGioHang
    {
        public List<string> maKoGiaTri()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from GioHang in db.GioHangs
                        orderby GioHang.MaGH ascending
                        select GioHang.MaGH).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaGioHang()
        {
            fashionDBEntities db = new fashionDBEntities();
            string macuoi = "";
            foreach (var item in new taoMaGioHang().maKoGiaTri())
            {
                macuoi = item.Substring(2, 3);
            }

            string ma1 = "GH";
            string s = "";

            if (db.GioHangs.Count() <= 0)
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