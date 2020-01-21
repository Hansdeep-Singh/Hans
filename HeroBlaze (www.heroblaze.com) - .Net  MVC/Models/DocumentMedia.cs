using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class DocumentMedia
    {
        public int DocumentMediaID { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentByte { get; set; }
        [ForeignKey("UserTalent")]
        public int UserTalentID { get; set; }
       
        public virtual UserTalent UserTalent { get; set; }
    }

    public class DocumentMediaViewModel
    {
        public int DocumentMediaID { get; set; }
        [Required(ErrorMessage ="Name of the document is required.")]
        public string DocumentName { get; set; }
        public int UserTalentID { get; set; }
        public string DocumentString { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}