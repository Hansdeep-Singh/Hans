using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientDetail
    {
        public int AdminClientDetailId { get; set; }
        [ForeignKey("Client")]
        [MaxLength(128)]
        public string ClientId { get; set; }
        public string ApplicationNumber {get;set;}
        public string ApplicationDetail {get;set;}
        public string Status { get; set; }
        public decimal? TotalFees {get;set;}
        public decimal? Paid {get;set;}
        public decimal? Balance {get;set;}
        public virtual ClientDetail ClientDetail { get; set; }
        public virtual Client Client { get; set; }
    }
}