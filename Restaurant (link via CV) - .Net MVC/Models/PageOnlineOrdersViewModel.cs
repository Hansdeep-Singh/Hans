using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class PageOnlineOrdersViewModel
    {
        public List<AllOrdersOfCustomer> Aooc_l { get; set; }
    }


    public class AllOrdersOfCustomer
    {
        public CustomerViewModel Cvm { get; set; }
        public List<AllOrders> Ao_l { get; set; }
       
    }

    public class AllOrders
    {
        public OrderViewModel Ovm { get; set; }
       
        public List<AllItemsOfOrder> Aioo_l { get; set; }
    }

    public class AllItemsOfOrder
    {
        public OrderItem Oi {get;set;}
        public List<MenuItemViewModel> Mivm_l { get; set; }
    }
   
}