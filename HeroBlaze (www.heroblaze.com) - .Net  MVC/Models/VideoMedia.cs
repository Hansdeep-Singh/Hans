using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class VideoMedia
    {
        public int VideoMediaID { get; set; }
        public string VideoTitle { get; set; }
        public string VideoLink { get; set; }
        [ForeignKey("UserTalent")]
        public int UserTalentID { get; set; }
        public virtual UserTalent UserTalent { get; set; }
    }
    public class VideoMediaViewModel
    {
        public int VideoMediaID { get; set; }
        public int UserTalentID { get; set; }
        [Required(ErrorMessage = "The Title of the video is required.")]
        public string VideoTitle { get; set; }
        [Required(ErrorMessage ="Please enter the youtube video link.")]
        public string VideoLink { get; set; }
    }
}