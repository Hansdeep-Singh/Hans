using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Logic;

namespace NepaleseRestaurant.Models
{
    public class Transfer
    {
        PhotoString ps = new PhotoString();
        public BlogViewModel BtoBVM(Blog b)
        {
            BlogViewModel bvm = new BlogViewModel();
            bvm.BlogId = b.BlogId;
            bvm.BlogDate = b.BlogDate;
            bvm.BlogFrom = b.BlogFrom;
            bvm.BlogMessage = b.BlogMessage;
            return bvm;
        }

        public Blog BVMtoB(BlogViewModel bvm)
        {
            Blog b  = new Blog();
            b.BlogId = bvm.BlogId;
            b.BlogDate = bvm.BlogDate;
            b.BlogFrom = bvm.BlogFrom;
            b.BlogMessage = bvm.BlogMessage;
            return b;
        }

        public MenuTypeViewModel MTtoMTVM(MenuType mt)
        {
            MenuTypeViewModel mtvm = new MenuTypeViewModel();
            mtvm.MenuTypeId = mt.MenuTypeId;
            mtvm.MenuTypeDescription = mt.MenuTypeDescription;
            mtvm.MenuTypeName = mt.MenuTypeName;
            return mtvm;
        }

        public MenuType MTVMtoMT(MenuTypeViewModel mtvm)
        {
            MenuType mt = new MenuType();
            mt.MenuTypeId = mtvm.MenuTypeId;
            mt.MenuTypeDescription = mtvm.MenuTypeDescription;
            mt.MenuTypeName = mtvm.MenuTypeName;
            return mt;
        }

        public MenuCategoryViewModel MCtoMCVM (MenuCategory mc)
        {
            MenuCategoryViewModel mcvm = new MenuCategoryViewModel();
            mcvm.MenuCategoryId = mc.MenuCategoryId;
            mcvm.MenuTypeId = mc.MenuTypeId;
            mcvm.MenuCategoryDescription = mc.MenuCategoryDescription;
            mcvm.MenuCategoryName = mc.MenuCategoryName;
            mcvm.MenuCategoryNameConcat = ps.CleanInput(mcvm.MenuCategoryName);
            return mcvm;
        }

        public MenuCategory MCVMtoMC(MenuCategoryViewModel mcvm)
        {
            MenuCategory mc = new MenuCategory();
            mc.MenuCategoryId = mcvm.MenuCategoryId;
            mc.MenuTypeId = mcvm.MenuTypeId;
            mc.MenuCategoryDescription = mcvm.MenuCategoryDescription;
            mc.MenuCategoryName = mcvm.MenuCategoryName;
            return mc;
        }

        public Reservation RVMtoR(ReservationViewModel rvm)
        {
            Reservation r = new Reservation();
            r.ReservationId = rvm.ReservationId;
            r.Name = rvm.Name;
            r.Email = rvm.Email;
            r.Subject = rvm.Subject;
            r.Adults = rvm.Adults;
            r.Date = rvm.Date;
            r.DateTimeRecieved = rvm.DateTimeRecieved;
            r.Kids = rvm.Kids;
            r.Message = rvm.Message;
            r.Phone = rvm.Phone;
            r.Time = rvm.Time;
            r.ReplyMessage = rvm.ReplyMessage;
            return r;
        }

        public ReservationViewModel RtoRVM(Reservation r)
        {
            ReservationViewModel rvm = new ReservationViewModel();
            rvm.ReservationId = r.ReservationId;
            rvm.Name = r.Name;
            rvm.Email = r.Email;
            rvm.Subject = r.Subject;
            rvm.Adults = r.Adults;
            rvm.Date = r.Date;
            rvm.DateTimeRecieved = r.DateTimeRecieved;
            rvm.Kids = r.Kids;
            rvm.Message = r.Message;
            rvm.Phone = r.Phone;
            rvm.Time = r.Time;
            rvm.ReplyMessage = r.ReplyMessage;
            return rvm;
        }

        public MenuItemViewModel MItoMIVM(MenuItem mi)
        {
            MenuItemViewModel mivm = new MenuItemViewModel();
            mivm.MenuItemId = mi.MenuItemId;
            mivm.MenuCategoryId = mi.MenuCategoryId;
            mivm.MenuItemDescription = mi.MenuItemDescription;
            mivm.MenuItemHalfFull = mi.MenuItemHalfFull;
            mivm.MenuItemMeat = mi.MenuItemMeat;
            mivm.MenuItemMisc = mi.MenuItemMisc;
            mivm.MenuItemName = mi.MenuItemName;
            mivm.MenuItemNumbering = mi.MenuItemNumbering;
            mivm.MenuItemPieces = mi.MenuItemPieces;
            mivm.MenuItemPrice = mi.MenuItemPrice;
            mivm.MenuItemServings = mi.MenuItemServings;
            mivm.MenuItemSpiciness = mi.MenuItemSpiciness;
            mivm.MenuItemStyle = mi.MenuItemStyle;
           // mivm.PhotoRawString = ByteToString(mi.MenuItemPhotoRaw);
            //mivm.PhotoLargeString = ByteToString(mi.MenuItemPhotoLarge);
            mivm.PhotoThumbString= ByteToString(mi.MenuItemPhotoThumb);
            return mivm;
        }

        public MenuItem MIVMtoMI(MenuItemViewModel mivm)
        {
            MenuItem mi = new MenuItem();
            mi.MenuItemId = mivm.MenuItemId;
            mi.MenuCategoryId = mivm.MenuCategoryId;
            mi.MenuItemDescription = mivm.MenuItemDescription;
            mi.MenuItemHalfFull = mivm.MenuItemHalfFull;
            mi.MenuItemMeat = mivm.MenuItemMeat;
            mi.MenuItemMisc = mivm.MenuItemMisc;
            mi.MenuItemName = mivm.MenuItemName;
            mi.MenuItemNumbering = mivm.MenuItemNumbering;
            mi.MenuItemPieces = mivm.MenuItemPieces;
            mi.MenuItemPrice = mivm.MenuItemPrice;
            mi.MenuItemServings = mivm.MenuItemServings;
            mi.MenuItemSpiciness = mivm.MenuItemSpiciness;
            mi.MenuItemStyle = mivm.MenuItemStyle;
            return mi;
        }

        public Order OvmToO(OrderViewModel ovm)
        {
            Order o = new Order();
            o.OrderId = ovm.OrderId;
            o.CustomerId = ovm.CustomerId;
            o.GrandTotal = ovm.GrandTotal;
            o.ProcessedDateTime = ovm.ProcessedDateTime;
            o.Instructions = ovm.Instructions;
            o.RecievedDateTime = ovm.RecievedDateTime;
            o.PickUpDeliever = ovm.PickUpDeliever;
            return o;
        }

        public OrderViewModel OToOvm(Order o)
        {
            OrderViewModel ovm = new OrderViewModel();
            ovm.OrderId = o.OrderId;
            ovm.CustomerId = o.CustomerId;
            ovm.GrandTotal = o.GrandTotal;
            ovm.ProcessedDateTime = o.ProcessedDateTime;
            ovm.Instructions = o.Instructions;
            ovm.RecievedDateTime = o.RecievedDateTime;
            ovm.PickUpDeliever = o.PickUpDeliever;
            return ovm;
        }

        public Customer CvmToC(CustomerViewModel cvm)
        {
            Customer c = new Customer();
            c.Address = cvm.Address;
            c.Name = cvm.Name;
            c.Phone = cvm.Phone;
            c.CustomerId = cvm.CustomerId;
            return c;
        }

        public CustomerViewModel CToCvm(Customer c)
        {
            CustomerViewModel cvm = new CustomerViewModel();
            cvm.Address = c.Address;
            cvm.Name = c.Name;
            cvm.Phone = c.Phone;
            cvm.CustomerId = c.CustomerId;
            return cvm;
        }


        public string ByteToString(Byte[] FileByte)
        {
            string File;
            if (FileByte == null) { File = ps.thumb; }
            else { File = ps.PhotoByteToString(FileByte); }
            return File;
        }

       

    }
}