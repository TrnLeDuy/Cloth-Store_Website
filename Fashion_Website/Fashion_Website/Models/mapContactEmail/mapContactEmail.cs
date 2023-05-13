using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Fashion_Website.Models.mapContactEmail
{
    public class mapContactEmail
    {
        //gửi Email thông báo hoàn thành thanh toán 
        public void SendEmail(string toEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("hkha928@gmail.com");
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com"; //hoặc smtp.live.com
                smtp.Port = 25; //hoặc 25
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("hkha928@gmail.com", "wzevsaqwbwjykllu");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}