using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using chatnyou.Data;
using chatnyou.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace chatnyou.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {

        public readonly ApplicationDbContext _db;

        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task SendMessage(string userfrmto, string message)
        {
           
            string[] split = userfrmto.Split(new char[] { '~' });
            string user = split[0];
            string ToId = split[1].Trim();
            var FromId = Context.UserIdentifier;
            
            await Clients.User(FromId).SendAsync("ReceiveMessage", user, message);
            await Clients.User(ToId).SendAsync("ReceiveMessage", user, message);
            Chat c = new Chat();
            Guid g = Guid.NewGuid();
            c.ChatId = g.ToString();
            c.Id = FromId;
            c.MsgFromId = ToId;
            c.MsgRead = true;
            c.Msg = message;
            c.MsgDteTme = DateTime.Now;
            await _db.Chats.AddAsync(c);
            await _db.SaveChangesAsync();
           
        }


        public async Task CheckNewUser() {
            bool result = false;
            int? usercount = _db.ChatUsers.Where(c => c.IsLoggedIn == true).Count();
            
            Counter c = _db.Counters.Where(cn => cn.CounterId == "counters").FirstOrDefault();
            int? countercount = c.UsrCntr;
            if (usercount != countercount)
            {
                c.UsrCntr = usercount;
                _db.Update(c);
                await _db.SaveChangesAsync();
                result = true;
            }

            await Clients.All.SendAsync("ResultCheckNewUser",result);
        }


        public async Task GetNewChats()
        {
            var UserId = Context.UserIdentifier;
            List<Chat> Chats = new List<Chat>();
            List<ChatUser> ChatUsers = new List<ChatUser>();
            List<string> FromUidL = new List<string>();
            var dte = DateTime.Now;
            var newdte = dte.Subtract(new TimeSpan(0,1,0));
            //Chats = _db.Chats.Where(id => id.Id == UserId && id.MsgDteTme > newdte).ToList();
            Chats = _db.Chats.Where(id => id.Id == UserId).ToList();
            foreach (var chat in Chats)
            {
                FromUidL.Add(chat.MsgFromId);
            }


            ChatUsers = _db.ChatUsers.Where(id=>FromUidL.Contains(id.Id)).ToList();
           await Clients.User(UserId).SendAsync("ReceiveNewChats", ChatUsers);
        }

        public async Task GetValidUsers()
        {
            //List<string> usl = new List<string>();
            //foreach( var val in  _db.ChatUsers.Where(c => c.IsLoggedIn == true).ToList())
            //{
            //    usl.Add(val.UserName);
            //}
            await Clients.All.SendAsync("ReceiveValidUsers", _db.ChatUsers.Where(c => c.IsLoggedIn == true).ToList());
        }
    }
}
