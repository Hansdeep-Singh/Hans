using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class MenuDisplayViewModel
    {
        public MenuTypeViewModel Mtvm { get; set; }
        public List<MenuCategoryMenuItems> Mcmi { get; set; }
    }

    public class MenuCategoryMenuItems
    {
        public MenuCategoryViewModel Mtvm { get; set; }
        public List<MenuItemViewModel> Mcvm { get; set; }
    }

}