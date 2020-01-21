using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class ImageMedia
    {
        public int ImageMediaID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }

        public byte[] ImageByteThumb { get; set; }
        [ForeignKey("UserTalent")]
        public int UserTalentID { get; set; }
        public virtual UserTalent UserTalent { get; set; }
    }

    public class ImageMediaViewModel
    {
        
        public int ImageMediaID { get; set; }
        public int UserTalentID { get; set; }
        [Required(ErrorMessage = "Name of the photo is required.")]
        public string ImageName { get; set; }
        public string PhotoString { get; set; }
        public string PhotoStringThumb { get; set; }
        public HttpPostedFileBase File { get; set; }
    }

}