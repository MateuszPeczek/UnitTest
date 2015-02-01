using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestProject.Models
{
    public class BlogPost : Entity<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class BlogPostDbContext : DbContext
    {
        public BlogPostDbContext() : base("Name=BlogPostDbContext") { }

        public virtual DbSet<BlogPost> BlogPosts { get; set; }
    }
}