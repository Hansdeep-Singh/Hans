using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace NepaleseRestaurant.Models
{
    public class Repository<T> where T : class
    {
        private bool disposed = false;
        public ApplicationDbContext Context = new ApplicationDbContext();
        protected DbSet<T> DbSet { get; set; }
        public Repository() {DbSet = Context.Set<T>();}
        public List<T> GetAll() { return DbSet.ToList(); }
        public T GetOneIntId(int? id) { return DbSet.Find(id); }
        public T GetOneStringId(string id) { return DbSet.Find(id); }
        public T GetOneOnEmail(string email) { return DbSet.Find(email); }
        public void Add(T entity) { DbSet.Add(entity); }
        public void Delete(T entity) { DbSet.Remove(entity); }
        public void DeleteRange(List<T> entity) { DbSet.RemoveRange(entity); }
        public void Save() { Context.SaveChanges(); }
        public void Entry(T entity) { Context.Entry(entity).State = EntityState.Modified; }
        public void EntryUnchanged(T entity) { Context.Entry(entity).State = EntityState.Unchanged; }
        public void EntryDetached(T entity) { Context.Entry(entity).State = EntityState.Detached; }
        public void EntryAdded(T entity) { Context.Entry(entity).State = EntityState.Added; }
        public virtual void Dispose() { if (!disposed) { Context.Dispose(); disposed = true; } }
    }
}