using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeroBlaze.Models
{
    public class DocumentRepository:Repository<DocumentMedia>
    {
        public void AddRow(DocumentMedia am)
        {
            Add(am); Save();
        }
        public DocumentMedia OneRow(int id)
        {
            return DbSet.Where(d => d.DocumentMediaID.Equals(id)).SingleOrDefault();
        }

        public IEnumerable<DocumentMedia> DocumentMediasIenum(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id));
        }
        public void EditDocument(DocumentMedia dm)
        {
            Entry(dm); Save();
        }
        public void DeleteDocument(DocumentMedia d)
        {
            Delete(d);
            Save();
        }

        public List<DocumentMedia> DocumentMediasList(int id)
        {
            return DbSet.Where(a => a.UserTalentID.Equals(id)).ToList();
        }
    }
}
