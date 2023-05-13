using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class UserController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        // GET: User
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin([Bind(Include = "Username, UserPass")] KHACHHANG khachhang)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(khachhang.Username))
                    return View(khachhang);
                if (string.IsNullOrEmpty(khachhang.UserPass))
                    return View(khachhang);
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var user = db.KHACHHANGs.FirstOrDefault(k => k.Username == khachhang.Username && k.UserPass == khachhang.UserPass);
                    if (user != null)
                    {
                        //Lưu thông vào session
                        if (user.TinhTrang == 0)
                        {
                            ViewBag.ThongBao = "Tài khoản này đã bị khóa!";
                        }

                        else
                        {
                            Session["KhachHang"] = user;
                            Session["UsernameKH"] = user.Username;
                            Session["FullnameKH"] = user.HoTen;
                            Session["IDKH"] = user.MaKH;
                            return Redirect("~/Home/TrangChu");
                        }
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(KHACHHANG user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username))
                    return View(user);
                if (string.IsNullOrEmpty(user.UserPass))
                    return View(user);
                //Kiểm tra xem có người nào đã đăng ký với tên đăng nhập này hay chưa
                var khachhang = db.KHACHHANGs.FirstOrDefault(k => k.Username == user.Username);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập này đã tồn tại");
                    ViewBag.ThongBao = "Tên đăng nhập này đã tồn tại";
                }
                if (ModelState.IsValid)
                {
                    db.KHACHHANGs.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Signin");
                }
            }
            
            return View();
        }

        //Đăng xuất
        public ActionResult Logout()
        {
            //Perform any necessary cleanup or logging out of the user
            //Remove any authentication cookies or session state information
            //Redirect the user to the login page
            Session["KhachHang"] = null;
            Session["Fullname"] = null;
            Session["Username"] = null;
            Session["ID"] = null;
            Session.Abandon();
            return RedirectToAction("Home/TrangChu");
        }
    }
}