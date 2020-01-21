using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeraCityImmigration.Models;
using System.Net.Mail;

namespace VeraCityImmigration.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string er)
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Team()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Downloads()
        {
            return View();
        }

        public ActionResult Testimonial()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add("suman@vimmigration.co.nz");
                mail.From = new MailAddress("expertsystems.co.nz@gmail.com");
                mail.Subject = "New Message";
                string Body = "Name Of Sender = " + c.Name + "<br/>" + "Phone number Of Sender = " + c.Phone + "Email Of Sender = " + c.Email + "<br/>" + "Subject = " + c.Subject + "<br/>" + "Message From Sender = " + c.Message;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("expertsystems.co.nz", "h4294967296");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                ViewBag.Error = "Your message has been sent to Vimmigration Services, Thanks for your interest.";
                return View();
            }
            ViewBag.Error = "Model Invalid";
            return View();
        }

        public ActionResult VisaProcessing()
        {
            return View();
        }

        public ActionResult ApplicationRequirements()
        {
            return View();


        }


    //     if (ModelState.IsValid)
    //        {
    //            MailMessage mail = new MailMessage();
    //    mail.To.Add("info@ssproduction.co.nz");
    //            mail.From = new MailAddress("expertsystems.co.nz@gmail.com");
    //    mail.Subject = "SS PRODUCTION MESSAGE";
    //            string Body = "Name Of Sender = " + gen.name + "<br/>" + "Email Of Sender = " + gen.email + "<br/>" + "Phone Number Of Sender = " + gen.phone + "<br/>" + "Message From Sender = " + gen.message;
    //    mail.Body = Body;
    //            mail.IsBodyHtml = true;
    //            SmtpClient smtp = new SmtpClient();
    //    smtp.Host = "smtp.gmail.com";
    //            smtp.Port = 587;
    //            smtp.UseDefaultCredentials = false;
    //            smtp.Credentials = new System.Net.NetworkCredential
    //            ("expertsystems.co.nz", "h4294967296");
    //            smtp.EnableSsl = true;
    //            smtp.Send(mail);
    //            ViewBag.Error = "Your message has been sent to SS PRODUCTION, Thanks for your interest.";
    //            return View();
    //}
    //ViewBag.Error = "Model Invalid";
    //        return View(gen);
}
}