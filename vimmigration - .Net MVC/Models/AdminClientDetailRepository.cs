using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientDetailRepository : Repository<AdminClientDetail>
    {
        public void AddRow(AdminClientDetail acd)
        {
            Add(acd);
            Save();
        }

        public AdminClientDetail OneRow(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id)).SingleOrDefault();
        }

        public void EditClient(AdminClientDetail acd)
        {
            Entry(acd); Save();
        }

    }
}