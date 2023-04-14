using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion_Website.Models;
using System.Data.Entity;

namespace Fashion_Website.Controllers
{
    public class AccountController : Controller
    {
        //DB Context
        fashionDBEntities db = new fashionDBEntities();

        //Generate User ID
        private string GenerateUserID()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999);
            return "KH" + randomNumber.ToString();
        }
        
        //create variable to hold the last MaKH
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
            int lastNumber;
            int.TryParse(lastMaKH.Substring(3), out lastNumber);
            lastNumber++;
            lastMaKH = "CUS" + lastNumber.ToString().PadLeft(7, '0');
            return lastMaKH;
        }
        /*View Changing ACTION*/
        public ActionResult DangKyView()
        {
            return View("DangKy");
        }

        public ActionResult DangNhapView()
        {
            return View("DangNhap");
        }

        /*ACTION ĐĂNG KÝ*/
        /*----------------------------------------------*/
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(User user, KhachHang khach)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(khach.HoTen))
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Họ và tên của bạn");
                if (string.IsNullOrEmpty(user.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(user.UserPass))  
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Căn cược công dân của bạn");
                if (string.IsNullOrEmpty(khach.SDT))
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Số điện thoại");
                if (string.IsNullOrEmpty(khach.Email))
                    ModelState.AddModelError(string.Empty, "Vui lòng nhập Email");
                //Kiểm tra xem có người nào đã đăng ký với tên đăng nhập này hay chưa
                var khachhang = db.Users.FirstOrDefault(k => k.Username == user.Username);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập này đã tồn tại");

                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    user.UserID = GenerateUserID();
                    user.Role = "KH";
                    user.UserID = khach.UserID;
                    user.TinhTrang = 1;
                    khach.MaKH = GenerateMaKH();
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
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