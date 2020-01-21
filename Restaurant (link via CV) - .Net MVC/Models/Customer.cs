using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Printed { get; set; }
        public bool Processed { get; set; }
        public bool Archive { get; set; }
    }

    public class CustomerViewModel
    {
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Phone number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$", ErrorMessage = "Please choose format (04-1234-1234)")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Name for the order is required.")]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string AddressLineOne { get; set; }
        [Required(ErrorMessage = "Suburb is required.")]
        public string Suburb { get; set; }
    }
}