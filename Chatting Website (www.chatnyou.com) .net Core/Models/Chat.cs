using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chatnyou.Models
{
    public class Chat
    {
        [Key]
        public string ChatId { get; set; }
        [ForeignKey("IdentityUser")]
        public string Id { get; set; }
        public string MsgFromId { get; set; }
        public string Msg { get; set; }
        public DateTime MsgDteTme { get; set; }
        public bool MsgRead { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
