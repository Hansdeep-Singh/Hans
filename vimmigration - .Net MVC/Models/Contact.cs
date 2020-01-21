using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class Contact
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Email addresss is required.")]
        [EmailAddress(ErrorMessage ="Please check the format of email address.")]
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        [Required(ErrorMessage ="Please enter your phone number.")]
        public string Phone { get; set; }
    }
}