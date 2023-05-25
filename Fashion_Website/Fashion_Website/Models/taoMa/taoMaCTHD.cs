using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.taoMa
{
    public class taoMaCTHD
    {
        public List<string> maDonKoGiaTri()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from ctHD in db.CTHOADONs
                        orderby ctHD.MACTHD ascending
                        select ctHD.MACTHD).ToList();
            return data;
        }
        public string TaoMaCTHD()
        {
            fashionDBEntities db = new fashionDBEntities();
            string macuoi = "";
            foreach (var item in new taoMaCTHD().maDonKoGiaTri())
            {
                macuoi = item.Substring(3, 7);
            }

            string ma1 = "CHD";
            string s = "";

            if (db.CTHOADONs.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "0000001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "000000"; }
                else if (k < 100)
                { s = s + "00000"; }
                else if (k < 1000)
                { s = s + "0000"; }
                else if (k < 10000)
                { s = s + "000"; }
                else if (k < 100000)
                { s = s + "00"; }
                else if (k < 1000000)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }
    }
}