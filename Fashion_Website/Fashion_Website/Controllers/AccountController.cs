using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Fashion_Website.Controllers
{
    public class AccountController : Controller
    {
        //DB Context
        private fashionDBEntities db = new fashionDBEntities();

        //create variable to hold the last MaKH
        private string prefix = "KH";
        private string lastMaKH;
        private string tempID;

        //Constructor to load the last MaKH from the database
        public AccountController()
        {
            var lastRecord = db.KHACHHANGs.OrderByDescending(x => x.MaKH).FirstOrDefault();
            if (lastRecord != null)
            {
                lastMaKH = lastRecord.MaKH;
            }
            else
            {
                // if no record is found, start with the first ID
                lastMaKH = prefix + "0000";
            }
        }
        //Generate MaKH by 1 everysingle add new
        private string GenerateMaKH()
        {
            if (string.IsNullOrEmpty(lastMaKH))
            {
                lastMaKH = prefix + "0001"; // assign a default starting value
                return lastMaKH;
            }

            int lastNumber;
            string numberPart = lastMaKH.Substring(prefix.Length);
            if (!int.TryParse(numberPart, out lastNumber))
            {
                {
                    lastNumber = 0;
                }
            }
            lastNumber++;
            int maxNumber = (int)Math.Pow(10, lastMaKH.Length - prefix.Length) - 1;
            lastNumber = Math.Min(lastNumber, maxNumber);
            lastMaKH = prefix + lastNumber.ToString().PadLeft(lastMaKH.Length - prefix.Length, '0');
            return lastMaKH;
        }

        [HttpGet]
        public ActionResult DangKyUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKyUser(USER user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                }
                if (string.IsNullOrEmpty(user.UserPass))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }
                //Kiểm tra xem có người nào đã đăng ký với tên đăng nhập này hay chưa
                var khachhang = db.USERS.FirstOrDefault(k => k.Username == user.Username);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập này đã tồn tại");
                    ViewBag.ThongBao = "Tên đăng nhập này đã tồn tại";
                }
                if (ModelState.IsValid)
                {
                    db.USERS.Add(user);
                    user.UserRole = "KH";
                    user.TinhTrang = 1;
                    user.MaKH = GenerateMaKH();
                    tempID = user.MaKH;
                    db.SaveChanges();
                    //return RedirectToAction("DangKyKhach", new { userId = user.UserID });
                    return RedirectToAction("DangKyThongTinKhach");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKyThongTinKhach()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKyThongTinKhach(KHACHHANG khach)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(khach.HoTen))
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Họ và tên của bạn");
                }
                if (string.IsNullOrEmpty(khach.SDT))
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Số điện thoại");
                }
                else if (!Regex.IsMatch(khach.SDT.Trim(), @"^[0-9]{10}$"))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không hợp lệ.");
                }
                if (string.IsNullOrEmpty(khach.Email))
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Email");
                }
                else if (!Regex.IsMatch(khach.Email.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                {
                    ModelState.AddModelError(string.Empty, "Email không hợp lệ. Vui lòng nhập email hợp lệ.");
                }
                if (string.IsNullOrEmpty(khach.CCCD))
                {
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập CCCD");
                }
                else if (!Regex.IsMatch(khach.CCCD.Trim(), @"^[0-9]{12}$"))
                {
                    ModelState.AddModelError(string.Empty, "CCCD không hợp lệ.");
                }
                if (ModelState.IsValid)
                {
                    khach.MaKH = tempID;
                    db.KHACHHANGs.Add(khach);
                    db.SaveChanges();

                    return RedirectToAction("DangNhap");
                }
            }
            return View(khach);
        }

        /*----------------------------------------------*/
        /*ACTION ĐĂNG NHẬP*/
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]

        public ActionResult DangNhap(USER users)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(users.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(users.UserPass))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                var user = db.USERS.FirstOrDefault(k => k.Username == users.Username && k.UserPass == users.UserPass);
                if (ModelState.IsValid)
                {
                    if (user != null)
                    {
                        //Lưu thông vào session
                        Session["Account"] = user;
                        Session["Fullname"] = user.KHACHHANG.HoTen;
                        Session["Username"] = user.Username;
                        Session["ID"] = user.MaKH;
                        user.MaKH = user.KHACHHANG.MaKH;
                        Session["Role"] = user.UserRole;
                        if (user.UserRole == "AD")
                            return Redirect("~/Dashboard/Dashboard");
                        else
                            return Redirect("~/Home/TrangChu");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["Fullname"] = null;
            Session["Username"] = null;
            Session["ID"] = null;
            Session["Role"] = null;
            Session.Abandon();
            return Redirect("/");
        }

        /*View Changing ACTION*/
        public ActionResult DangKyView()
        {
            return View("DangKyUser");
        }

        public ActionResult DangNhapView()
        {
            return View("DangNhap");
        }
    }
}