using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class ClientDetailViewModel
    {

        public int ClientDetailId { get; set; }
        [MaxLength(128)]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string Lastname { get; set; }

        [Required]
        [RegularExpression(@"(^$)|(^\d{2}/\d{2}/\d{4})|(^((\d{1})|(\d{2}))/((\d{1})|(\d{2}))/(\d{4})\s((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))\s((AM)|(PM)))", ErrorMessage = "Correct the format,thanks.")]
        public string dob { get; set; }
        public string address { get; set; }
        [Required(ErrorMessage = "Phone Number Is Required")]
        public string phone { get; set; }
        public string visatype { get; set; }
        [RegularExpression(@"(^$)|(^\d{2}/\d{2}/\d{4})|(^((\d{1})|(\d{2}))/((\d{1})|(\d{2}))/(\d{4})\s((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))\s((AM)|(PM)))", ErrorMessage = "Correct the format, thanks.")]
        public string visaduedate { get; set; }
        public string passportcountry { get; set; }
        [RegularExpression(@"(^$)|(^\d{2}/\d{2}/\d{4})|(^((\d{1})|(\d{2}))/((\d{1})|(\d{2}))/(\d{4})\s((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))\s((AM)|(PM)))", ErrorMessage = "Correct the format, thanks.")]
        public string passportexpirydate { get; set; }
        [RegularExpression(@"(^$)|(^\d{2}/\d{2}/\d{4})|(^((\d{1})|(\d{2}))/((\d{1})|(\d{2}))/(\d{4})\s((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))[:]{1}((\d{1})|(\d{2}))\s((AM)|(PM)))", ErrorMessage = "Correct the format, thanks.")]
        public string policeclearanceexpiry { get; set; }
        public string medicalexpiry { get; set; }
        [Required(ErrorMessage = "Passport Number is Required")]
        public string passportnumber { get; set; }
        public byte[] photoraw { get; set; }
        public string rawstring { get; set; }
        public byte[] photo { get; set; }
        public string photostring { get; set; }
        public byte[] photothumb { get; set; }
        public string thumbstring { get; set; }
       // [ValidateFile(ErrorMessage = "Please select a image of correct format and smaller than 1MB")]
        public HttpPostedFileBase file { get; set; }
    }
}