﻿using Fashion_Website.Models;
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
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include ="Username, UserPass")] ADMIN adUser) 
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(adUser.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(adUser.UserPass))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //Tìm người dùng có tên đăng nhập và password hợp lệ trong CSDL
                    var user = db.ADMINs.FirstOrDefault(k => k.Username == adUser.Username && k.UserPass == adUser.UserPass);
                    if (user != null)
                    {
                        //Lưu thông vào session
                        Session["Account"] = user;
                        Session["Username"] = user.Username;
                        Session["Fullname"] = user.HoTen;
                        Session["ID"] = user.MaAD;
                        Session["Role"] = user.ChucVu;
                        if (user.TinhTrang == 0)
                        {
                            ViewBag.ThongBao = "Tài khoản này đã bị khóa!";
                        }
                        else
                            return Redirect("~/Dashboard/Dashboard");
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
            return RedirectToAction("/");
        }
    }
}