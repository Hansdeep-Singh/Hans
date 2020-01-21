using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NepaleseRestaurant.Models
{
    public class MenuCategory
    {
        [Required()]
        public string MenuCategoryId { get; set; }
        [ForeignKey("MenuType")]
        [MaxLength(128)]
        public string MenuTypeId { get; set; }
        public string MenuCategoryName { get; set; }
        public string MenuCategoryDescription { get; set; }
        public virtual MenuType MenuType { get; set; }
    }

    public class MenuCategoryViewModel
    {
        public string MenuCategoryId { get; set; }
        public string MenuTypeId { get; set; }
        [Required(ErrorMessage = "Add category name")]
        public string MenuCategoryName { get; set; }
        public string MenuCategoryNameConcat { get; set; }
        public string MenuCategoryDescription { get; set; }
    }
}