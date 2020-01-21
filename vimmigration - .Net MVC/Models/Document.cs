using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        [MaxLength(128)]
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public byte[] document { get; set; }
        public string date { get; set; }
        public virtual Client Client { get; set; }
    }
}