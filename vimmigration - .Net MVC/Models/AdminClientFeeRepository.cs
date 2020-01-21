using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeraCityImmigration.Models
{
    public class AdminClientFeeRepository : Repository<AdminClientFee>
    {
        public void AddFee(AdminClientFee f)
        {
            Add(f); Save();
        }
        public AdminClientFee OneRow(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id)).SingleOrDefault();
        }

        public AdminClientFee OneRow(int id)
        {
            return DbSet.Where(a => a.AdminClientFeeId.Equals(id)).SingleOrDefault();
        }

        public void DeleteInstallment(AdminClientFee acf)
        {
            Delete(acf);
            Save();
        }
        public IEnumerable<AdminClientFee> AdminClientFees(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id));
        }
    }
}