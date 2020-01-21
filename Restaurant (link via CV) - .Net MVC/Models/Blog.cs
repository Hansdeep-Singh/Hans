using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NepaleseRestaurant.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string BlogFrom { get; set; }
        public string BlogDate { get; set; }
        public string BlogMessage { get; set; }
    }

    public class BlogViewModel
    {
        public int BlogId { get; set; }
        [Required(ErrorMessage ="Your Name Is Required")]
        public string BlogFrom { get; set; }
        public string BlogDate { get; set; }
        [Required(ErrorMessage = "Please express your thoughts by writting few words. Thanks")]
        public string BlogMessage { get; set; }
    }
}