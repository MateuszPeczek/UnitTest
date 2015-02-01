using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;

namespace TestProject.Repository
{
    public class BlogRepository : GenericRepository<BlogPost>, IBlogRepository
    {

        public BlogRepository(DbContext context) : base(context) { }

        public override IEnumerable<BlogPost> GetAll()
        {
            return _entities.Set<BlogPost>().ToList();
        }

        public BlogPost SelectById(int? id)
        {
            return _dbset.Find(id);
        }
    }
}