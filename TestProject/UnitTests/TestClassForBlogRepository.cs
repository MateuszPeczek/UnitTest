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