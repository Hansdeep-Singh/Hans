using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientFee
    {
        public int AdminClientFeeId { get; set; }
        [ForeignKey("Client")]
        [MaxLength(128)]
        public string ClientId { get; set; }
        public decimal? Installment { get; set; }
        public string InstallmentDate { get; set; }
        public string InstallmentType { get; set; }
        public virtual Client Client { get; set; }

    }
}