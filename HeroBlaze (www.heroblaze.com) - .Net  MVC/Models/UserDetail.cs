using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class UserDetail
    {
        public int UserDetailID { get; set; }
        [ForeignKey("HeroBlazeUser")]
        [MaxLength(128)]
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserAbout { get; set; }
        public int DobYear { get; set; }
        public int DobMonth { get; set; }
        public int DobDay { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public byte[] ProfilePhotoThumb { get; set; }
        public byte[] PhotoRaw { get; set; }
        [Timestamp()]
        public byte[] RowVersion { get; set; }
        public virtual HeroBlazeUser HeroBlazeUser { get; set; } 
    }

    public class UserDetailViewModel
    {
        public int UserDetailID { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Write Something About Yourself")]
        public string UserAbout { get; set; }
        public int DobYear { get; set; }
        public int DobMonth { get; set; }
        public int DobDay { get; set; }
        [Timestamp()]
        public byte[] RowVersion { get; set; }
        public string PhotoString { get; set; }
        public string PhotoStringThumb { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}