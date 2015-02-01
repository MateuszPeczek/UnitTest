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
                new BlogPost {Id = 3, Title="Title3", Content="Content3"},
                new BlogPost {Id = 4, Title="Title4", Content="Content4"},
                new BlogPost {Id = 5, Title="Title5", Content="Content5"},
                new BlogPost {Id = 6, Title="Title6", Content="Content6"},
                new BlogPost {Id = 7, Title="Title7", Content="Content7"},
                new BlogPost {Id = 8, Title="Title8", Content="Content8"},
                new BlogPost {Id = 9, Title="Title9", Content="Content9"},
                new BlogPost {Id = 10, Title="Title10", Content="Content10"},
                new BlogPost {Id = 11, Title="Title11", Content="Content11"},
                new BlogPost {Id = 12, Title="Title12", Content="Content12"}
            };

            foreach (BlogPost bp in blogs)
            {
                context.BlogPosts.Add(bp);
            }
        }
    }
}
