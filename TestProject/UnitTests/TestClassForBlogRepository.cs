using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;
using FakeItEasy;
using TestProject.Models;
using TestProject.Repository;
using System.Data.Entity;

namespace TestProject.UnitTests
{
    /*public class TestClassForBlogRepository
    {
        private BlogRepository fakeRepository = null;
        private IBlogRepository fakeIRepository = null;
        private DbSet<BlogPost> fakeDbSet = A.Fake<DbSet<BlogPost>>();
        private BlogPostDbContext fakeDbContext = A.Fake<BlogPostDbContext>();

        public TestClassForBlogRepository()
        {
            fakeIRepository = A.Fake<IBlogRepository>();
            fakeRepository = new BlogRepository(fakeDbContext);
        }

        [Fact]
        private void Repository_SelectAll_Test()
        {
            var data = new List<BlogPost>
            {
                new BlogPost {ID = 1, Title="Title1", Content="Content1"},
                new BlogPost {ID = 2, Title="Title2", Content="Content2"},
                new BlogPost {ID = 2, Title="Title3", Content="Content3"}
            }.AsQueryable<BlogPost>();

            var fakeDbSet = A.Fake<DbSet<BlogPost>>();

            A.Fake<DbSet<BlogPost>>(builder => builder.Implements(typeof(IQueryable<BlogPost>)));

            A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).Provider).Returns(data.Provider);
            A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).Expression).Returns(data.Expression);
            A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).ElementType).Returns(data.ElementType);
            A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).GetEnumerator()).Returns(data.GetEnumerator());

            var fakeDbContext = A.Fake<BlogPostDbContext>();
            A.CallTo(() => fakeDbContext.BlogPosts).Returns(fakeDbSet);

            fakeRepository = new BlogRepository(fakeDbContext);
            var listBlog = fakeRepository.SelectAll();

        }

        [Fact]
        private void Repository_Create_Test()
        {
            fakeRepository.Create(A<BlogPost>._);

            A.CallTo(() => fakeDbContext.BlogPosts.Add(A<BlogPost>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        private void Repository_SelectByID_Test()
        {
            fakeRepository.SelectByID(A<int>._);

            A.CallTo(() => fakeDbContext.BlogPosts.Find(A<int>._)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        private void Repository_Update_Test()
        {
            var blogEntry = new BlogPost() { ID = 1, Content = "Content", Title = "Title" };
            fakeRepository.Update(blogEntry);

            Assert.Equal(fakeDbContext.Entry(blogEntry).State, System.Data.Entity.EntityState.Modified);
        }

        [Fact]
        private void Repository_Delete_Test()
        {
            var blogEntry = new BlogPost() { ID = 1, Content = "Content", Title = "Title" };
            fakeRepository.Delete(1);

            A.CallTo(() => fakeDbContext.BlogPosts.Find(1)).Returns(new BlogPost() { ID = 1, Content = "Content", Title = "Title" });

            A.CallTo(() => fakeDbContext.BlogPosts.Remove(blogEntry)).MustHaveHappened(Repeated.Exactly.Once);

        }

        [Fact]
        private void Repository_Save_Test()
        {
            fakeRepository.Save();
            A.CallTo(() => fakeDbContext.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
        }
    }*/
}