using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientDetailViewModel
    {
        public int AdminClientDetailId { get; set; }
        public string ClientId { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string ApplicationNumber {get;set;}
        public string ApplicationDetail {get;set;}
        public decimal? TotalFees {get;set;}
        public decimal? Paid {get;set;}
        public decimal? Balance {get;set;}
        public string dob { get; set; }
        public string Status { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string visatype { get; set; }
        public string visaduedate { get; set; }
        public string passportcountry { get; set; }
        public string passportexpirydate { get; set; }
        public string policeclearanceexpiry { get; set; }
        public string medicalexpiry { get; set; }
        public string passportnumber { get; set; }
        public byte[] photothumb { get; set; }
        public byte[] photo { get; set; }
        public byte[] photoraw { get; set; }
        public string thumbstring { get; set; }

    }
}