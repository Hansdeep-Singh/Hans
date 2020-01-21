using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace VeraCityImmigration.Models
{
    public class Repository<T> where T : class
    {
        private ApplicationDbContext Context = new ApplicationDbContext();
        protected DbSet<T> DbSet { get; set; }
        public Repository() { DbSet = Context.Set<T>(); }
        public List<T> GetAll() { return DbSet.ToList(); }
        public T GetOne(int? id) { return DbSet.Find(id); }
        //public T GetOneOnId(string id) { return DbSet.Find(id); }
        public T GetOne (string email) { return DbSet.Find(email); }
        public void Add(T entity) { DbSet.Add(entity); }
        public void Delete(T entity) { DbSet.Remove(entity); }
        public void Save() { Context.SaveChanges(); }
        public void Entry(T entity) { Context.Entry(entity).State = EntityState.Modified; }
        public void EntryUnchanged(T entity) { Context.Entry(entity).State = EntityState.Unchanged; }
        public void EntryDetached(T entity) { Context.Entry(entity).State = EntityState.Detached; }
        public void EntryAdded(T entity) { Context.Entry(entity).State = EntityState.Added; }

    }
}
