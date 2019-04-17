using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace QE.Services
{
    public class MailService
    {
        public static void SendNewPassword(string to, string userId, string password)
        {
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("qeqe0928@gmail.com");

            // The important part -- configuring the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // [1] You can try with 465 also, I always used 587 and got success
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; // [2] Added this
            smtp.UseDefaultCredentials = false; // [3] Changed this
            smtp.Credentials = new NetworkCredential(mail.From.Address, "qe20172017");  // [4] Added this. Note, first parameter is NOT string.
            smtp.Host = "smtp.gmail.com";

            //recipient address
            mail.To.Add(new MailAddress(to));

            //Formatted mail body
            mail.IsBodyHtml = true;
            mail.Subject = "QE account";
            mail.Body = "User ID: "+userId+"<br>";
            mail.Body += "Passowrd: " + password;

            smtp.Send(mail);
        }
    }
}