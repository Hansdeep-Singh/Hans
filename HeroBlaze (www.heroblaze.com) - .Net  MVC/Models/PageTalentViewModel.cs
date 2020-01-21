using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class PageTalentViewModel
    {
        public UserTalentViewModel utvm { get; set; }
        public AudioMediaViewModel amvm { get; set; }
        public DocumentMediaViewModel dmvm { get; set; }
        public VideoMediaViewModel vmvm { get; set; }
        public ImageMediaViewModel imvm { get; set; }
    }
}