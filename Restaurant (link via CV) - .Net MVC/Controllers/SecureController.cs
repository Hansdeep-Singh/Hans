using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NepaleseRestaurant.Models;
using Business.Logic;
using System.IO;
using System.Net.Mail;
using System.Media;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Drawing.Printing;
//using Microsoft.AspNet.SignalR;

namespace NepaleseRestaurant.Controllers
{
    [Authorize]
    public class SecureController : Controller
    {
        // GET: Secure
        Transfer t = new Transfer();
        MenuTypeRepository mtr = new MenuTypeRepository();
        MenuCategoryRepository mcr = new MenuCategoryRepository();
        MenuItemRepository mir = new MenuItemRepository();
        BlogRepository br = new BlogRepository();
        ReservationRepository rr = new ReservationRepository();
        ResizeImage ri = new ResizeImage();
        PhotoString ps = new PhotoString();
        FileString fs = new FileString();
        OrderItemRepository oir = new OrderItemRepository();
        OrderRepository or = new OrderRepository();
        CustomerRepository cr = new CustomerRepository();
        MessageRepository mr = new MessageRepository();

        [HttpGet]
        public ActionResult Index()
        {
            PageOnlineOrdersViewModel Poovm = new PageOnlineOrdersViewModel();
            List<AllOrdersOfCustomer> Aooc_L = new List<AllOrdersOfCustomer>();

            foreach (var customer in cr.AllCustomers().Where(c => c.Processed == false))
            {
                //Orders underneath
                AllOrdersOfCustomer Aooc = new AllOrdersOfCustomer();
                Aooc.Cvm = t.CToCvm(customer);
                List<AllOrders> Ao_L = new List<AllOrders>();

                foreach (var order in or.OrdersOnCustomerId(customer.CustomerId).Where(p => p.Processed == false))
                {
                    //Order Items underneath
                    AllOrders Ao = new AllOrders();
                    Ao.Ovm = t.OToOvm(order);
                    List<AllItemsOfOrder> Aioo_L = new List<AllItemsOfOrder>();
                    foreach (var orderitem in oir.OrderItemsOnOrderId(order.OrderId))
                    {
                        //Menu Items underneath
                        AllItemsOfOrder Aioo = new AllItemsOfOrder();
                        Aioo.Oi = orderitem;
                        List<MenuItemViewModel> Mivm_L = new List<MenuItemViewModel>();
                        foreach (var menuitem in mir.MenuItems(orderitem.MenuItemId))
                        {
                            Mivm_L.Add(t.MItoMIVM(menuitem));
                        }
                        Aioo.Mivm_l = Mivm_L;
                        Aioo_L.Add(Aioo);
                       
                    }
                    Ao.Aioo_l = Aioo_L;
                    Ao_L.Add(Ao);
                }
                Aooc.Ao_l = Ao_L;
                Aooc_L.Add(Aooc);
                ////////////
            }
            Poovm.Aooc_l = Aooc_L;
            cr.Dispose();
            return View(Poovm);
        }

        public ActionResult OrderProcessed(string id)
        {
            Order o = new Order();
            o = or.Order(id);
            o.Processed = true;
            or.UpdateOrder(o);
            or.Dispose();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerProcessed(string id)
        {
            Customer c = new Customer();
            c = cr.Customer(id);
            c.Processed = true;
            cr.UpdateCustomer(c);
            cr.Dispose();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ManageBlogs()
        {
            List<BlogViewModel> bvml = new List<BlogViewModel>();
            foreach (var blog in br.GetAllBlogs())
            {
                bvml.Add(t.BtoBVM(blog));
            }
            br.Dispose();
            return View(bvml);
        }

        public ActionResult DeleteBlog(int id)
        {
            br.DeleteBlog(br.Blog(id));
            br.Dispose();
            return RedirectToAction("ManageBlogs");
        }
        [HttpGet]
        public ActionResult ManageMessages()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageMessages(Message m)
        {
            Message mup = new Message();
            mup = mr.MessageOnTitle(m.MessageTitle);
            mup.TheMessage = m.TheMessage;
            mr.UpdateMessage(mup);
            return View();
        }

        public ActionResult ManageMenu()
        {
            MenuTypeViewModel mtvm = new MenuTypeViewModel();
            List<PageManageMenuViewModel> Pmmvm_list = new List<PageManageMenuViewModel>();
            foreach (var menutype in mtr.GetAllMenuTypes())
            {
                PageManageMenuViewModel Pmmvm = new PageManageMenuViewModel();
                mtvm = t.MTtoMTVM(menutype);
                List<MenuCategoryViewModel> Mcvm_list = new List<MenuCategoryViewModel>();
                foreach (var menucategory in mcr.MenuCategoriesOnMenuType(mtvm.MenuTypeId))
                {
                    Mcvm_list.Add(t.MCtoMCVM(menucategory));
                }
                Pmmvm.Mtvm = mtvm;
                Pmmvm.Mcvm = Mcvm_list;
                Pmmvm_list.Add(Pmmvm);
            }
            ViewBag.MenuTypesAndCategories = Pmmvm_list;
            mtr.Dispose();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuTypePost(MenuTypeViewModel mtvm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("ManageMenu", "Secure"); }
            MenuType mt = new MenuType();
            mt = t.MTVMtoMT(mtvm);
            mt.MenuTypeId = Guid.NewGuid().ToString();
            mtr.AddMenuType(mt);
            mtr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MenuCategoryPost(MenuCategoryViewModel mcvm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("ManageMenu", "Secure"); }

            MenuCategory mc = new MenuCategory();
            mc = t.MCVMtoMC(mcvm);
            mc.MenuCategoryId = Guid.NewGuid().ToString();
            mcr.AddMenuCategory(mc);
            mcr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }

        [HttpPost]
        public ActionResult DeleteCategory(string id)
        {
            if (!Result(id) && !mcr.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            List<MenuItem> Mi_list = new List<MenuItem>();
            Mi_list = mir.MenuItemsOnCategoryId(id);
            mir.DeleteMenuItems(Mi_list);
            MenuCategory mc = new MenuCategory();
            mc = mcr.MenuCategory(id);
            mcr.DeleteMenuCategory(mc);
            mcr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }


        public ActionResult DeleteType(string id)
        {
            if (!Result(id) && !mtr.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            List<MenuCategory> Mc_list = new List<MenuCategory>();
            Mc_list = mcr.MenuCategoriesOnMenuTypeId(id);
            mcr.DeleteMenuCategories(Mc_list);
            MenuType mt = new MenuType();
            mt = mtr.MenuType(id);
            mtr.DeleteMenuType(mt);
            mtr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }

        public ActionResult EditType(string id)
        {
            if (!Result(id) && !mtr.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            MenuTypeViewModel mtvm = new MenuTypeViewModel();
            mtvm = t.MTtoMTVM(mtr.MenuType(id));
            mtr.Dispose();
            return View(mtvm);
        }
        [HttpPost]
        public ActionResult EditType(MenuTypeViewModel mtvm)
        {
            if (!ModelState.IsValid) { return View(mtvm); }
            mtr.UpdateMenuType(t.MTVMtoMT(mtvm));
            mtr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }


        [Route("Secure/MenuCategory/{id?}")]
        public ActionResult MenuCategory(string id)
        {
            if (!Result(id) || !mcr.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            PageMenuCategoryViewModel pmcvm = new PageMenuCategoryViewModel();
            pmcvm.Mcvm = t.MCtoMCVM(mcr.MenuCategory(id));

            MenuItemViewModel mivm = new MenuItemViewModel();
            mivm.MenuCategoryId = id;
            pmcvm.Mivm = mivm;

            List<MenuItemViewModel> Mivm_list = new List<MenuItemViewModel>();
            int i = 0;
            foreach (var menuitem in mir.MenuItemsOnCategoryId(id))
            {
                MenuItemViewModel mivml = new MenuItemViewModel();
                mivml = t.MItoMIVM(menuitem);
                i = i + 1;
                mivml.MenuItemOrder = i;
                Mivm_list.Add(mivml);

            }
            pmcvm.Mivml = Mivm_list;
            mir.Dispose();
            return View(pmcvm);
        }

        public ActionResult ManageReservations()
        {
            PageReservationViewModel prvm = new PageReservationViewModel();
            List<ReservationViewModel> rvml = new List<ReservationViewModel>();
            foreach (var reservation in rr.GetAllReservations())
            {
                rvml.Add(t.RtoRVM(reservation));
            }

            prvm.Rvml = rvml;
            rr.Dispose();
            return View(prvm);
        }
        [HttpPost]
        public ActionResult ReservationReply(ReservationViewModel rvm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("ManageReservations", "Secure"); }
            rr.UpdateReservation(t.RVMtoR(rvm));
            MailMessage mail = new MailMessage();
            mail.To.Add(rvm.Email);
            mail.From = new MailAddress("expertsystems.co.nz@gmail.com");
            mail.Subject = "Nepalese and Indian Restaurant RESERVATION CONFIRMATION";
            string Body = rvm.ReplyMessage;
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
            return RedirectToAction("ManageReservations");
        }


        [HttpPost]
        public ActionResult DeleteReservation(string id)
        {
            if (!Result(id) && !mir.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            Reservation r = rr.Reservation(id);
            rr.DeleteReservation(r);
            rr.Dispose();
            return RedirectToAction("ManageReservations", "Secure");
        }


        [HttpPost]
        public ActionResult DeleteItem(string id)
        {
            if (!Result(id) && !mir.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            MenuItem mi = mir.MenuItem(id);
            TempData["CatId"] = mi.MenuCategoryId;
            mir.DeleteMenuItem(mi);
            mir.Dispose();
            return RedirectToAction("MenuCategory", "Secure", new { id = TempData["CatId"] });
        }

        public ActionResult EditCategory(string id)
        {
            if (!Result(id) && !mcr.IfExists(id)) { return RedirectToAction("NullResult", "Secure"); }
            MenuCategoryViewModel mcvm = new MenuCategoryViewModel();
            mcvm = t.MCtoMCVM(mcr.MenuCategory(id));
            mcr.Dispose();
            return View(mcvm);
        }
        [HttpPost]
        public ActionResult EditCategory(MenuCategoryViewModel mcvm)
        {
            if (!ModelState.IsValid) { return View(mcvm); }
            mcr.UpdateMenuCategory(t.MCVMtoMC(mcvm));
            mcr.Dispose();
            return RedirectToAction("ManageMenu", "Secure");
        }

        [HttpPost]
        public ActionResult AddItemPost(MenuItemViewModel mivm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("MenuCategory", "Secure", new { id = mivm.MenuCategoryId }); }
            MenuItem mi = new MenuItem();
            mi = t.MIVMtoMI(mivm);
            MemoryStream ms = new MemoryStream();
            if (mivm.File != null)
            {
                mivm.File.InputStream.CopyTo(ms);
            }

            mi.MenuItemPhotoRaw = ms.ToArray();
            mi.MenuItemPhotoLarge = ri.ResizeFromStream(750, ms, "Raw750");
            mi.MenuItemPhotoThumb = ri.ResizeFromStream(250, ms, "Raw250");
            mi.MenuItemId = Guid.NewGuid().ToString();
            mir.AddMenuItem(mi);
            mir.Dispose();
            return RedirectToAction("MenuCategory", "Secure", new { id = mivm.MenuCategoryId });
        }

        [HttpPost]
        //public ActionResult EditItemPost(MenuItemViewModel mivm)
        public ActionResult EditItemPost(MenuItemViewModel mivm)
        {
            if (!ModelState.IsValid) { return RedirectToAction("MenuCategory", "Secure", new { id = mivm.MenuCategoryId }); }
            MenuItem mi = new MenuItem();
            mi = t.MIVMtoMI(mivm);
            mi.MenuItemHalfFull = fs.isnah(mivm.MenuItemHalfFull);
            mi.MenuItemMeat = fs.isnah(mivm.MenuItemMeat);
            mi.MenuItemPieces = fs.isnah(mivm.MenuItemPieces);
            mi.MenuItemServings = fs.isnah(mivm.MenuItemServings);
            mi.MenuItemSpiciness = fs.isnah(mivm.MenuItemSpiciness);
            mi.MenuItemStyle = fs.isnah(mivm.MenuItemStyle);

            MemoryStream ms = new MemoryStream();

            if (mivm.File != null)
            {
                mivm.File.InputStream.CopyTo(ms);
                mi.MenuItemPhotoRaw = ms.ToArray();
                mi.MenuItemPhotoLarge = ri.ResizeFromStream(750, ms, "Raw750");
                mi.MenuItemPhotoThumb = ri.ResizeFromStream(250, ms, "Raw250");
            }
            else
            {
                MenuItem mib = new MenuItem();
                mib = mir.MenuItem(mivm.MenuItemId);
                mi.MenuItemPhotoLarge = mib.MenuItemPhotoLarge;
                mi.MenuItemPhotoRaw = mib.MenuItemPhotoRaw;
                mi.MenuItemPhotoThumb = mib.MenuItemPhotoThumb;
                mir.EntryDetached(mib);
            }

            mir.UpdateMenuItem(mi);
            mir.Dispose();
            return RedirectToAction("MenuCategory", "Secure", new { id = mivm.MenuCategoryId });
        }

        public ActionResult Crop(string id)
        {
            MenuItem mi = new MenuItem();
            mi = mir.MenuItem(id);
            ImageCoordinates i = new ImageCoordinates();
            i.MenuItemId = mi.MenuItemId;
            i.PhotoString = ps.PhotoByteToString(mi.MenuItemPhotoLarge);
            return View(i);
        }

        [HttpPost]
        public ActionResult Crop(ImageCoordinates i)
        {
            MenuItem mi = new MenuItem();
            ResizeImage ri = new ResizeImage();
            int x1 = Convert.ToInt16(i.x1);
            int x2 = Convert.ToInt16(i.x2);
            int y1 = Convert.ToInt16(i.y1);
            int y2 = Convert.ToInt16(i.y2);
            int w = Convert.ToInt16(i.w);
            int h = Convert.ToInt16(i.h);
            byte[] CroppedPhoto;
            if (w != 0 || h != 0)
            {
                CroppedPhoto = ImageHelper.CropImage(mir.MenuItem(i.MenuItemId).MenuItemPhotoLarge, x1, y1, w, h);
                Stream MemStr = new MemoryStream(CroppedPhoto);
                mi = mir.MenuItem(i.MenuItemId);
                mi.MenuItemPhotoThumb = ri.ResizeFromStream(200, MemStr, "CroppedPhoto");
                mir.UpdateMenuItem(mi);
            }

            return RedirectToAction("MenuCategory",new {id = mi.MenuCategoryId });
        }


        public ActionResult ManageOrders()
        {
            return View();
        }

        public Boolean Result(string id)
        {
            if (id == null) { return false; }
            else { return true; }
        }

        public ActionResult NullResult()
        {
            return View();
        }




        //MenuTypeRepository mtr = new MenuTypeRepository();
       
       
       
        
        //ResizeImage ri = new ResizeImage();
        //PhotoString ps = new PhotoString();
        //FileString fs = new FileString();
      
        
        //CustomerRepository cr = new CustomerRepository();


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (mtr != null)
                {
                    mtr.Dispose();
                    mtr = null;
                }
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




        //private void SendMessage(string message)
        // {
        //     GlobalHost
        //    .ConnectionManager
        //    .GetHubContext<MyHub>().Clients.sendMessage(
        //  message);
        // }

    }
}