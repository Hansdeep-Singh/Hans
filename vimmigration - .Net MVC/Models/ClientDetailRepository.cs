using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeraCityImmigration.Models;

namespace VeraCityImmigration.Models
{
    public class ClientDetailRepository : Repository<ClientDetail>
    {
        public void AddRow(ClientDetail cd)
        {
            Add(cd);
            Save();
        }
        public ClientDetail OneRow(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id)).SingleOrDefault();
        }
        public void EditClient(ClientDetail cd)
        {
            Entry(cd);Save();
        }

        public void UpdateClient(ClientDetail cd, ClientDetailViewModel cdvm)
        {
            DbSet.Attach(cd);
            cd.ClientId = cdvm.ClientId;
            cd.ClientDetailId = cdvm.ClientDetailId;
            cd.ClientId = cdvm.ClientId;
            cd.FirstName = cdvm.FirstName;
            cd.Lastname = cdvm.Lastname;
            cd.dob = cdvm.dob;
            cd.address = cdvm.address;
            cd.phone = cdvm.phone;
            cd.visatype = cdvm.visatype;
            cd.visaduedate = cdvm.visaduedate;
            cd.passportcountry = cdvm.passportcountry;
            cd.passportexpirydate = cdvm.passportexpirydate;
            cd.passportnumber = cdvm.passportnumber;
            cd.policeclearanceexpiry = cdvm.policeclearanceexpiry;
            cd.medicalexpiry = cdvm.medicalexpiry;
            Add(cd);
            Save();
            
        }
    }
}