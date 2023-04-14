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
        fashionDBEntities db = new fashionDBEntities();

        /*View Changing ACTION*/
        public ActionResult DangKyView()
        {
            return View("DangKyUser");
        }

        public ActionResult DangNhapView()
        {
            return View("DangNhap");
        }

        /*ACTION ĐĂNG KÝ*/
        /*----------------------------------------------*/
        [HttpGet]
        public ActionResult DangKyUser()
        {
            return View();
        }

        protected string tempuid;
        //Generate User ID
        private string GenerateUserID()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999);
            return "KH" + randomNumber.ToString();
        }

        //create variable to hold the last MaKH
        private string prefix = "CUS";
        private string lastMaKH;
        //Constructor to load the last MaKH from the database
        public AccountController()
        {
            var lastRecord = db.KhachHangs.OrderByDescending(x => x.MaKH).FirstOrDefault();
            if (lastRecord != null)
            {
                lastMaKH = lastRecord.MaKH;
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

        [HttpPost]
        public ActionResult DangKyUser(User user)
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
                var khachhang = db.Users.FirstOrDefault(k => k.Username == user.Username);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập này đã tồn tại");
                }
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    user.UserID = GenerateUserID();
                    user.Role = "KH";
                    user.TinhTrang = 1;
                    db.SaveChanges();
                    //return RedirectToAction("DangKyKhach", new { userId = user.UserID });
                    return RedirectToAction("DangNhap");
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKyKhach(string userId)
        {
            // retrieve the user from the database using the UserID parameter passed in
            var user = db.Users.Find(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            // create a new KhachHang object and set its UserID property to the UserID passed in
            var khach = new KhachHang { UserID = userId };
            db.KhachHangs.Add(khach);
            return View(khach);
        }

        [HttpPost]
        public ActionResult DangKyKhach(KhachHang khach)
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
                    db.KhachHangs.Add(khach);
                    khach.MaKH = GenerateMaKH();
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
        public ActionResult DangNhap(User users)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(users.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(users.UserPass))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var user = db.Users.FirstOrDefault(k => k.Username == users.Username && k.UserPass == users.UserPass);
                    if (user != null)
                    {
                        //Lưu thông vào session
                        Session["Account"] = user;
                        if (user.Role == "AD")
                            return View("~/Views/Admin/Dashboard.cshtml");
                        else
                            return View("~/Views/Home/Index.cshtml");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["Account"] = null;
            Session.Abandon();
            return RedirectToAction("DangNhap", "Account");
        }
    }
}