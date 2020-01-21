using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NepaleseRestaurant.Models;
using System.Net.Mail;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NepaleseRestaurant.Controllers
{

    //send grid api key = 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
    public class HomeController : Controller
    {
        private Transfer t = new Transfer();
        private BlogRepository br = new BlogRepository();
        private ReservationRepository rr = new ReservationRepository();
        private MenuItemRepository mir = new MenuItemRepository();
        private MenuCategoryRepository mcr = new MenuCategoryRepository();
        private OrderRepository or = new OrderRepository();
        private CustomerRepository cr = new CustomerRepository();
        private OrderItemRepository oir = new OrderItemRepository();
        private MessageRepository mr = new MessageRepository();
        private AllDispose adp = new AllDispose();
        public ActionResult Index()
        {
            ViewBag.Messages = mr.AllMessages();
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult MenuTakeAway()
        {
            return View();
        }

        public ActionResult Reservation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation(ReservationViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                Reservation r = new Reservation();
                r = t.RVMtoR(rvm);
                r.ReservationId = Guid.NewGuid().ToString();
                rr.AddReservation(r);
                rr.Dispose();
                MailMessage mail = new MailMessage();
                mail.To.Add("info@mountaingate.com.au");
                mail.From = new MailAddress("expertsystems.co.nz@gmail.com");
                mail.Subject = "Nepalese and Indian Restaurant RESERVATION";
                string Body = "Name Of Sender = " + rvm.Name + "<br/>" + "Email Of Sender = " + rvm.Email + "<br/>" + "Phone Number = " + rvm.Phone + "<br/>" + "Subject = " + rvm.Subject + "<br/>" + "No of Adults = " + rvm.Adults + "<br/>" + "No of Kids = " + rvm.Kids + "<br/>" + "On Date = " + rvm.Date + "<br/>" + " At Time=" + rvm.Time + "<br/>" + "Message From Sender = " + rvm.Message;
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
                ViewBag.Message = "Your Reservation has been sent. Many Thanks.";
                return View();
            }
            return View(rvm);
        }
        [HttpGet]
        public ActionResult Blog()
        {
            List<BlogViewModel> bvm = new List<BlogViewModel>();
            foreach (var blog in br.GetAllBlogs())
            {
                bvm.Add(t.BtoBVM(blog));
            }
            br.Dispose();
            ViewBag.Blogs = bvm;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlogPost(BlogViewModel bvm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Blog", "Home"); }
            Blog b = new Blog();
            b = t.BVMtoB(bvm);
            b.BlogDate = DateTime.Now.ToString();
            br.AddBlog(b);
            br.Dispose();
            return RedirectToAction("Blog", "Home");
        }
        [HttpGet]
        public ActionResult Order()
        {
            PageOrderViewModel Povm = new PageOrderViewModel();
            List<CategoryItem> CI_l = new List<CategoryItem>();
            try
            {
                foreach (var menucategory in mcr.AllMenuCategories())
                {
                    CategoryItem CI = new CategoryItem();

                    CI.Mcvm = t.MCtoMCVM(menucategory);
                    List<MenuItemViewModel> mivm_l = new List<MenuItemViewModel>();
                    //foreach (var menuitem in mir.MenuItemsOnCategoryId(menucategory.MenuCategoryId))
                       foreach (var menuitem in mir.OrderMenuItemsOnCategoryId(menucategory.MenuCategoryId))
                        {
                            mivm_l.Add(t.MItoMIVM(menuitem));
                        }
                    CI.Mivm_L = mivm_l;
                    CI_l.Add(CI);
                };
                Povm.CI_L = CI_l;
            }
            finally
            {
                mcr.Dispose();
            }
            return View(Povm);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Fill in the form.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact c)
        {
            if (ModelState.IsValid)
            {
                try { Execute(c).Wait(); }
                finally {ViewBag.Message = "Message Sent.";}
                Execute(c).Wait();
                return View();
            }
            return View(c);
        }

       public static async Task Execute(Contact c)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(c.Email, c.Name);
            var subject = "Contact message from website.";
            var to = new EmailAddress("info@mountaingate.com.au", "Ram Pokharel");
            var plainTextContent = c.Name;
            var htmlContent = "<strong>"+c.Message+"</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        
        [HttpPost]
        public ActionResult FinalPost(PageOrderViewModel Povm)
        {
            string[] ids = (string[])TempData["ids"];
            string[] qty = (string[])TempData["qty"];
            Customer c = new Customer();
            Order o = new Order();
            c = t.CvmToC(Povm.Cust);
            c.Address = Povm.Cust.AddressLineOne + " " + Povm.Cust.Suburb;
            o = t.OvmToO(Povm.Odr);
            o.OrderId = Guid.NewGuid().ToString();

            //o.RecievedDateTime = DateTime.Now.ToString();
            for (int i = 0; i < ids.Length; i++)
            {
                o.GrandTotal += mir.MenuItem(ids[i]).MenuItemPrice * Convert.ToInt16(qty[i]);
            }
            if (o.PickUpDeliever == "Delivery") { o.GrandTotal += 5; };
            c.CustomerId = o.CustomerId = Guid.NewGuid().ToString();
            cr.AddCustomer(c);

            or.AddOrder(o);
            int ghg = ids.Length;

            for (int i = 0; i < ids.Length; i++)
            {
                OrderItem oi = new OrderItem();
                oi.OrderItemId = Guid.NewGuid().ToString();
                oi.OrderId = o.OrderId;
                oi.MenuItemId = ids[i];
                oi.Quantity = Convert.ToInt16(qty[i]);
                oi.Total = mir.MenuItem(ids[i]).MenuItemPrice * oi.Quantity;
                oir.AddOrderItem(oi);
            }

            //TempData["msg"] = "<script>alert('Order Sent');</script>";

            string confirmationmessage;
            confirmationmessage = "<script> swal({title: 'Great!',text: 'Your order has been sent through!',icon: 'success',button: 'Ok',});</script>";
            TempData["msg"] = confirmationmessage;
           
            //SendMessage("Notification");
            oir.Dispose();

            return RedirectToAction("Order");
        }


        public ActionResult DinnerForTwo(PageOrderViewModel Povm)
        {

            Customer c = new Customer();
            Order o = new Order();
            c = t.CvmToC(Povm.Cust);
            c.Address = Povm.Cust.AddressLineOne + " " + Povm.Cust.Suburb;
            o = t.OvmToO(Povm.Odr);
            o.OrderId = Guid.NewGuid().ToString();
            string instructions = "Dinner For Two >> " + o.Instructions;
            o.Instructions = instructions;
            o.GrandTotal = 36;
            c.CustomerId = o.CustomerId = Guid.NewGuid().ToString();
            cr.AddCustomer(c);

            or.AddOrder(o);


            string confirmationmessage;
            confirmationmessage = "<script> swal({title: 'Great!',text: 'Your order has been sent through!',icon: 'success',button: 'Ok',});</script>";
            TempData["msg"] = confirmationmessage;
            //SendMessage("Notification");
            oir.Dispose();

            return RedirectToAction("Order");
        }

        public ActionResult FamilyPack(PageOrderViewModel Povm)
        {

            Customer c = new Customer();
            Order o = new Order();
            c = t.CvmToC(Povm.Cust);
            c.Address = Povm.Cust.AddressLineOne + " " + Povm.Cust.Suburb;
            o = t.OvmToO(Povm.Odr);
            o.OrderId = Guid.NewGuid().ToString();
            string instructions = "Family-Pack >> " + o.Instructions;
            o.Instructions = instructions;
            o.GrandTotal = 60;
            c.CustomerId = o.CustomerId = Guid.NewGuid().ToString();
            cr.AddCustomer(c);

            or.AddOrder(o);


            string confirmationmessage;
            confirmationmessage = "<script> swal({title: 'Great!',text: 'Your order has been sent through!',icon: 'success',button: 'Ok',});</script>";
            TempData["msg"] = confirmationmessage;
            //SendMessage("Notification");
            oir.Dispose();

            return RedirectToAction("Order");
        }


        public ActionResult LunchPack(PageOrderViewModel Povm)
        {

            Customer c = new Customer();
            Order o = new Order();
            c = t.CvmToC(Povm.Cust);
            o = t.OvmToO(Povm.Odr);
            o.OrderId = Guid.NewGuid().ToString();
            o.RecievedDateTime = DateTime.Now.ToString();
            string instructions = "Lunch-Pack >> " + Povm.Odr.Curry + " " + Povm.Odr.Drinks + " Rice";
            o.Instructions = instructions;
            o.GrandTotal = 10;
            c.CustomerId = o.CustomerId = Guid.NewGuid().ToString();
            cr.AddCustomer(c);
            or.AddOrder(o);


            string confirmationmessage;
            confirmationmessage = "<script> swal({title: 'Great!',text: 'Your order has been sent through!',icon: 'success',button: 'Ok',});</script>";
            TempData["msg"] = confirmationmessage;
            //SendMessage("Notification");
            oir.Dispose();

            return RedirectToAction("Order");
        }

        //public void SendMessage(string message)
        //{
        //    GlobalHost
        //   .ConnectionManager
        //   .GetHubContext<MyHub>().Clients.sendMessage(message);
        //}

        [HttpPost]
        public void OrderPost(string[] ids, string[] qty)
        {

            TempData["Ids"] = ids;
            TempData["Qty"] = qty;

            string test = mir.IfExists(ids[1]).ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (br != null)
                {
                    br.Dispose();
                    br = null;
                }
                if (rr != null)
                {
                    rr.Dispose();
                    rr = null;
                }

                if (mir != null)
                {
                    mir.Dispose();
                    mir = null;
                }

                if (mcr != null)
                {
                    mcr.Dispose();
                    mcr = null;
                }

                if (or != null)
                {
                    or.Dispose();
                    or = null;
                }

                if (cr != null)
                {
                    cr.Dispose();
                    cr = null;
                }

                if (oir != null)
                {
                    oir.Dispose();
                    oir = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}



//c.Date = DateTime.Now.ToString();
//MailMessage mail = new MailMessage();
//mail.To.Add("info@mountaingate.com.au");
//mail.From = new MailAddress(c.Email);
//mail.Subject = "Nepalese and Indian Restaurant Contact Page";
//string Body = "Name Of Sender = " + c.Name + "<br/>" + "Email Of Sender = " + c.Email + "<br/>" + "Subject = " + c.Subject + "<br/>" + "On Date/Time = " + c.Date + "<br/>" + "Message From Sender = " + c.Message;
//mail.Body = Body;
//mail.IsBodyHtml = true;
//SmtpClient smtp = new SmtpClient();
//smtp.Host = "smtp.gmail.com";
//smtp.Port = 587;
//smtp.UseDefaultCredentials = false;
//smtp.Credentials = new System.Net.NetworkCredential
//("expertsystems.co.nz", "h4294967296");
//smtp.EnableSsl = true;
//smtp.Send(mail);