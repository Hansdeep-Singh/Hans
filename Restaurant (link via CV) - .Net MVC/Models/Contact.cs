using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NepaleseRestaurant.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Required.")]
        [EmailAddress(ErrorMessage = "Please Check Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required, please add a subject.")]
        public string Subject { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
    }
}


