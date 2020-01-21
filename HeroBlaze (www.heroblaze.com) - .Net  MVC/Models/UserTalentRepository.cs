using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class UserTalentRepository:Repository<UserTalent>
    {
        public void AddRow(UserTalent ut)
        {
            Add(ut); Save();
        }
        public UserTalent OneRow(string id)
        {
            return DbSet.Where(a => a.UserID.Equals(id)).SingleOrDefault();
        }

        public UserTalent OneRowTalent(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<UserTalent> UserTalentsIenum(string id)
        {
            return DbSet.Where(a => a.UserID.Equals(id));
        }
        public void EditTalent(UserTalent ut)
        {
            Entry(ut); Save();
        }
        public void DeleteTalent(UserTalent ut)
        {
            Delete(ut);
            Save();
        }

        public List<UserTalent> UserTalentsList(string id)
        {
            return DbSet.Where(a => a.UserID.Equals(id)).ToList();
        }
    }
}