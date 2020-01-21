using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class AudioMedia
    {
        public int AudioMediaID { get; set; }
        [ForeignKey("UserTalent")]
        public int UserTalentID { get; set; }
        public string AudioTitle { get; set; }
        public string Description { get; set; }
        public byte[] AudioByte { get; set; }
        public virtual UserTalent UserTalent { get; set; }
    }

    public class AudioMediaViewModel
    {
        public int AudioMediaID { get; set; }
        public int UserTalentID { get; set; }
        public int SongIndex { get; set; }
        [Required(ErrorMessage ="Audio Title is required.")]
        public string AudioTitle { get; set; }
        [Required(ErrorMessage ="Description is required.")]
        public string Description { get; set; }
        public byte[] AudioByte { get; set; }
        public string AudioFile { get; set; }
        public decimal Duration { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}


//https://www.youtube.com/watch?v=PMTUnBsBiaw
//https://www.youtube.com/watch?v=B5Sq33FIcUo