using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chatnyou.Models
{
    public class Counter
    {
        [Key]
        public string CounterId { get; set; }
        public Int32? MsgCntr { get; set; }
        public Int32? UsrCntr { get; set; }

    }
}
