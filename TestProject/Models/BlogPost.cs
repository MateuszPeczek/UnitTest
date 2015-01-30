using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class BlogPost
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class BlogPostDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}