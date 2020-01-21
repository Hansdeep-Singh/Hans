using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeraCityImmigration.Models;

namespace VeraCityImmigration.Models
{
    public class DocumentRepository:Repository<Document>
    {
        public void AddDocument(Document d)
        {
            Add(d);Save();
        }
        public Document OneRow(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<Document> Documents(string id)
        {
            return DbSet.Where(a => a.ClientId.Equals(id));
        }
        public Document OneRow(int id)
        {
            return DbSet.Where(a => a.DocumentId.Equals(id)).SingleOrDefault();
        }

        //public List<Document> Documents(string clientid)
        //{
        //    return DbSet.Where(a=>a.ClientId.Equals(clientid)).ToList();
        //}

      
        public void DeleteDocument(Document d)
        {
            Delete(d);
            Save();
        }
    }
}