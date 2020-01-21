using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        [MaxLength(128)]
        public string ClientId { get; set; }
        [Required(ErrorMessage ="Document name is required")]
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public byte[] document { get; set; }
        public string date { get; set; }
      //  [ValidateFile(ErrorMessage = "Please select a PNG image smaller than 1MB")]
        public HttpPostedFileBase file { get; set; }

    }
}