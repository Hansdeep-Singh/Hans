using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class PageOrderViewModel
    {
        public CustomerViewModel Cust { get; set; }
        public OrderViewModel Odr { get; set; }
        public List<CategoryItem> CI_L { get; set; }
       
    }

    public class CategoryItem
    {
        public MenuCategoryViewModel Mcvm { get; set; }
        public List<MenuItemViewModel> Mivm_L { get; set; }
    }




}