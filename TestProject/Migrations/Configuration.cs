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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TestProject.Models.BlogPostDbContext context)
        {
            List<BlogPost> defaultEntries = new List<BlogPost>();

            defaultEntries.Add(new BlogPost() { ID = 1, Title = "Title1", Content = "Content1" });
            defaultEntries.Add(new BlogPost() { ID = 2, Title = "Title2", Content = "Content2" });
            defaultEntries.Add(new BlogPost() { ID = 3, Title = "Title3", Content = "Content3" });
            defaultEntries.Add(new BlogPost() { ID = 4, Title = "Title4", Content = "Content4" });
            defaultEntries.Add(new BlogPost() { ID = 5, Title = "Title5", Content = "Content5" });
            defaultEntries.Add(new BlogPost() { ID = 6, Title = "Title6", Content = "Content6" });
            defaultEntries.Add(new BlogPost() { ID = 7, Title = "Title7", Content = "Content7" });

            foreach (BlogPost blog in defaultEntries)
                context.BlogPosts.Add(blog);

            base.Seed(context);
        }
    }
}
