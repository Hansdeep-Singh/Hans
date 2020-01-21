using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class UserInitialInfoRepository:Repository<UserDetail>
    {
       
        public void AddRow(UserDetail ud)
        {
            Add(ud);Save();
        }
        public UserDetail OneRow(string id)
        {
            return DbSet.Where(a => a.UserID.Equals(id)).SingleOrDefault();
        }
        public void EditUser(UserDetail ud)
        {
            Entry(ud);Save();
        }

        public void EditFields(UserDetail ud)
        {
            Context.Entry(ud).Property(ud.FirstName).IsModified = true;
            Context.Entry(ud).Property(ud.LastName).IsModified = true;
            Save();
        }
        
    }


}