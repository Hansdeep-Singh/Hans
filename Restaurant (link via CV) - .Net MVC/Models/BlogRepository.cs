using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NepaleseRestaurant.Models
{
    public class BlogRepository:Repository<Blog>
    {
        public void AddBlog(Blog b)
        {
            Add(b); Save();
        }

        public Blog Blog(int id)
        {
            return DbSet.Where(d => d.BlogId.Equals(id)).SingleOrDefault();
        }

        public List<Blog> GetAllBlogs()
        {
            return DbSet.ToList();
        }

        public void EntryDetatch(Blog b)
        {
            EntryDetached(b);
        }

        public void EntryAttach(Blog b)
        {
            EntryAttach(b);
        }

        public List<Blog> Blogs(string id)
        {
            return DbSet.Where(i => i.BlogId.Equals(id)).ToList();
        }

        public void DeleteBlog(Blog b)
        {
            Delete(b); Save();
        }


        public void UpdateBlog(Blog b)
        {
            Entry(b); Save();
        }

        public Boolean IfExists(int id)
        {
            if (DbSet.Any(d => d.BlogId.Equals(id)))
                return true;
            else
                return false;
        }

    }
}