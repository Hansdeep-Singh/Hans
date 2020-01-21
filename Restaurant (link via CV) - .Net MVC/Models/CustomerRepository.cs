using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class CustomerRepository:Repository<Customer>
    {

        public void AddCustomer(Customer c)
        {
            Add(c); Save();
        }

        public Customer Customer(string id)
        {
            return DbSet.Where(c => c.CustomerId.Equals(id)).SingleOrDefault();
        }

        public List<Customer> GetAllCustomers()
        {
            return DbSet.ToList();
        }

        public void EntryDetatch(Customer c)
        {
            EntryDetached(c);
        }

        public void EntryAttach(Customer c)
        {
            EntryAttach(c);
        }

        public List<Customer> AllCustomers()
        {
            return DbSet.ToList();
        }
        public List<Customer> Customers(string id)
        {
            return DbSet.Where(i => i.CustomerId.Equals(id)).ToList();
        }


        public List<Customer> CustomersOnCustomerId(string id)
        {
            return DbSet.Where(i => i.CustomerId.Equals(id)).ToList();
        }


        public void DeleteCustomer(Customer c)
        {
            Delete(c); Save();
        }

        public void DeleteCustomers(List<Customer> c_list)
        {
            DeleteRange(c_list); Save();
        }

        public Boolean IfExists(string id)
        {
            if (DbSet.Any(ie => ie.CustomerId.Equals(id)))
                return true;
            else
                return false;
        }


        public void UpdateCustomer(Customer c)
        {
            Entry(c); Save();
        }
    }
}
