using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NepaleseRestaurant.Models
{
    public class Reservation
    {
        [Required()]
        public string ReservationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public int Adults { get; set; }
        public int Kids { get; set; }
        public int Phone { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }

        public string ReplyMessage { get; set; }
        public string DateTimeRecieved { get; set; }
    }

    public class ReservationViewModel
    {
        public string ReservationId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("[a-zA-Z ]+$", ErrorMessage = "Please no website or links")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email Required.")]
        [EmailAddress(ErrorMessage = "Please Check Email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required, please add a subject.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "No of adults is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please use numbers only.")]
        public int Adults { get; set; }
        //[Required(ErrorMessage = "No of kids is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please use numbers only.")]
        public int Kids { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please use numbers only.")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Time is required")]
        public string Time { get; set; }
        [RegularExpression("[a-zA-Z ]+$", ErrorMessage ="Please no website or links (Avoid Period, thanks)")]
        public string Message { get; set; }
        public string ReplyMessage { get; set; }
        public string DateTimeRecieved { get; set; }
    }
}