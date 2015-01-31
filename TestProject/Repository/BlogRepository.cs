using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private BlogPostDbContext _db = null;

        public BlogRepository()
        {
            _db = new BlogPostDbContext();
        }

        public BlogRepository(BlogPostDbContext db)
        {
            _db = db;
        }

        public IEnumerable<BlogPost> SelectAll()
        {
            return _db.BlogPosts.ToList();
        }

        public BlogPost SelectByID(int? id)
        {
            return _db.BlogPosts.Find(id);
        }

        public void Create(BlogPost obj)
        {
            _db.BlogPosts.Add(obj);
        }

        public void Update(BlogPost obj)
        {
            _db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int? id)
        {
            BlogPost existing = _db.BlogPosts.Find(id);
            _db.BlogPosts.Remove(existing);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}