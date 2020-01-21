using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace chatnyou.Models
{
    public class ChatUser
    {
        [Key]
        public string ChatUserId { get; set; }
        [ForeignKey("IdentityUser")]
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool IsLoggedIn { get; set; }
        public DateTime LoggedOn { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }

        public string Sex { get; set; }
        

        public int Age { get; set; }

        public string Country { get; set; }

        public string State { get; set; }
    }

    public enum Sex
    {
        Male,
        Female
    }


}
