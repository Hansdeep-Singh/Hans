using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class ClientDetail
    {
        public int ClientDetailId { get; set; }
        
        [ForeignKey("Client")]
        [MaxLength(128)]
        public string ClientId { get; set; }
        [MaxLength(128)]
        public string FirstName { get; set; }
        [MaxLength(128)]
        public string Lastname { get; set; }
        [MaxLength(128)]
        public string dob { get; set; }
        [MaxLength(228)]
        public string address { get; set; }
        [MaxLength(128)]
        public string phone { get; set; }
        [MaxLength(128)]
        public string visatype { get; set; }
        [MaxLength(128)]
        public string visaduedate { get; set; }
        [MaxLength(128)]
        public string passportcountry { get; set; }
        [MaxLength(128)]
        public string passportexpirydate { get; set; }
        [MaxLength(128)]
        public string policeclearanceexpiry { get; set; }
        [MaxLength(128)]
        public string medicalexpiry { get; set; }
        
        [MaxLength(128)]
        public string passportnumber { get; set; }
        public byte[] photothumb { get; set; }
        public byte[] photo { get; set; }
        public byte[] photoraw { get; set; }
        public virtual Client Client { get; set; }
    }
}