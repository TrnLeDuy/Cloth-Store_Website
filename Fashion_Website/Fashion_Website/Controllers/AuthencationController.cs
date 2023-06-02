using Fashion_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fashion_Website.Controllers
{
    public class AuthencationController : Controller
    {
        private fashionDBEntities db = new fashionDBEntities();

        public ActionResult Index()
        {
            return View();
        }


        
        // GET: Authencation
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include ="Username, UserPass")] ADMIN adUser) 
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(adUser.Username))
                    return View(adUser);
                if (string.IsNullOrEmpty(adUser.UserPass))
                    return View(adUser);   
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var user = db.ADMINs.FirstOrDefault(k => k.Username == adUser.Username && k.UserPass == adUser.UserPass);
                    if (user != null)
                    {
                        if (user.TinhTrang == 0)
                        {
                            ViewBag.ThongBao = "Tài khoản này đã bị khóa!";
                        }
                        else
                        {
                            //Lưu thông vào session
                            Session["Account"] = user;
                            Session["Username"] = user.Username;
                            Session["Fullname"] = user.HoTen;
                            Session["ID"] = user.MaAD;
                            Session["Role"] = user.ChucVu;
                            Session["Avatar"] = user.avatar;
                            return Redirect("~/Dashboard/Dashboard");
                        } 
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
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
            Session["Account"] = null;
            Session["Fullname"] = null;
            Session["Username"] = null;
            Session["ID"] = null;
            Session["Role"] = null;
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string repeatPassword)
        {
            ADMIN admin = db.ADMINs.Find(Convert.ToInt32(Session["ID"].ToString()));

            if (admin == null)
            {
                return RedirectToAction("Login");
            }

            if (newPassword != repeatPassword)
            {
                TempData["Error"] = "Mật khẩu mới không khớp với nhau!";
                return View();
            }

            if (admin.UserPass != oldPassword)
                TempData["Error"] = "Vui lòng kiểm tra lại mật khẩu !";
            return View();
            }
        }
}