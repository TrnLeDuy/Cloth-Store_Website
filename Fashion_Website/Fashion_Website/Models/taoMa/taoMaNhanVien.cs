﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fashion_Website.Models.taoMa
{
    public class taoMaNhanVien
    {
        public List<string> maKoGiaTri()
        {
            fashionDBEntities db = new fashionDBEntities();
            var data = (from ADMIN in db.ADMINs
                        orderby ADMIN.MaAD ascending
                        select ADMIN.MaAD).ToList();
            return data;
        }
        //Tạo mã
        public string TaoMaNhanVien()
        {
            fashionDBEntities db = new fashionDBEntities();
            string macuoi = "";
            foreach (var item in new taoMaNhanVien().maKoGiaTri())
            {
                macuoi = item.Substring(3, 7);
            }

            string ma1 = "AD";
            string s = "";

            if (db.ADMINs.Count() <= 0)
            {
                s = Convert.ToString((ma1 + "00000001"));
                return s;
            }
            else
            {
                int k;
                s = ma1;
                k = Convert.ToInt32(macuoi);
                k = k + 1;
                if (k < 10)
                { s = s + "0000000"; }
                else if (k < 100)
                { s = s + "000000"; }
                else if (k < 1000)
                { s = s + "00000"; }
                else if (k < 10000)
                { s = s + "0000"; }
                else if (k < 100000)
                { s = s + "000"; }
                else if (k < 1000000)
                { s = s + "00"; }
                else if (k < 10000000)
                { s = s + "0"; }

                s = s + k.ToString();

                return s;
            }
        }
    }
}