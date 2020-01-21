using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class UserTalent
    {
        public int UserTalentID { get; set; }
        [ForeignKey("HeroBlazeUser")]
        [MaxLength(128)]
        public string UserID { get; set; }
        public string TalentDescription { get; set; }
        public string TalentName { get; set; }
        public virtual HeroBlazeUser HeroBlazeUser { get; set; }
        public virtual List<AudioMedia> AudioMedias { get; set; }
        public virtual List<DocumentMedia> DocumentMedias { get; set; }
        public virtual List<ImageMedia> ImageMedias { get; set; }
        public virtual List<VideoMedia> VideoMedias { get; set; }

    }

    public class UserTalentViewModel
    {
        public int UserTalentID { get; set; }
        public string UserID { get; set; }
        public string TalentDescription { get; set; }
        public string TalentName { get; set; }
       
    }
}