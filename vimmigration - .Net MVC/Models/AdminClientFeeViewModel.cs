using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientFeeViewModel
    {
        public int AdminClientFeeId { get; set; }
        [ForeignKey("Client")]
        [MaxLength(128)]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "Installment ammount is required")]
        public decimal? Installment { get; set; }
        public decimal? Sum { get; set; }
        [MaxLength(128)]
        public string InstallmentDate { get; set; }
        [MaxLength(128)]
        public string InstallmentType { get; set; }
      

    }
}