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
    public class TestClassForBlogRepository
    {
        private BlogRepository fakeRepository = null;
        private IBlogRepository fakeBlogList = null;
        private DbSet<BlogPost> fakeDbSet = A.Fake<DbSet<BlogPost>>();
        private BlogPostDbContext fakeDbContext = A.Fake<BlogPostDbContext>();

        public TestClassForBlogRepository()
        {
            fakeBlogList = A.Fake<IBlogRepository>();
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
            }.AsEnumerable();

            var fakeDbSet = A.Fake<DbSet<BlogPost>>();

            

            //A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).Provider).Returns(data.Provider);
            //A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).Expression).Returns(data.Expression);
            //A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).ElementType).Returns(data.ElementType);
            A.CallTo(() => ((IQueryable<BlogPost>)fakeDbSet).GetEnumerator()).Returns(data.GetEnumerator());

            var fakeDbContext = A.Fake<BlogPostDbContext>();
            A.CallTo(() => fakeDbContext.BlogPosts).Returns(fakeDbSet);

            fakeRepository = new BlogRepository(fakeDbContext);
            var listBlog = fakeRepository.SelectAll();

            //Assert.Equal("Title1", listBlog);

        }

        [Fact]
        private void Repository_Create_Test()
        {
            A.CallTo(() => fakeDbContext.BlogPosts).Returns(fakeDbSet);
            var post = new BlogPost() {ID = 1, Title="Title", Content="Content"};
            fakeRepository.Create(post);

            A.CallTo(() => fakeDbContext.BlogPosts.Add(post)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        private void Repository_SelectByID_Test()
        {
            var expectedResult = new BlogPost() { ID = 1, Title = "Title", Content = "Content" };
            fakeRepository.Create(expectedResult);
            var result = fakeRepository.SelectByID(1);

            A.CallTo(() => fakeDbContext.BlogPosts.Find(1)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}