namespace TestProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TestProject.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TestProject.Models.BlogPostDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TestProject.Models.BlogPostDbContext context)
        {
            var blogs = new List<BlogPost>
            {
                new BlogPost {Id = 1, Title="Title1", Content="Content1"},
                new BlogPost {Id = 2, Title="Title2", Content="Content2"},
                new BlogPost {Id = 3, Title="Title3", Content="Content3"}
            };

            foreach (BlogPost bp in blogs)
            {
                context.BlogPosts.Add(bp);
            }
        }
    }
}
