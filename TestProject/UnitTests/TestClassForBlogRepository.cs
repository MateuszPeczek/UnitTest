using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using TestProject.Models;
using TestProject.Repository;
using Xunit;

namespace TestProject.UnitTests
{
    public class TestClassForBlogRepository
    {
        DbConnection connection;
        TestContext databaseContext;
        BlogRepository objRepository;

        public TestClassForBlogRepository()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            databaseContext = new TestContext(connection);
            objRepository = new BlogRepository(databaseContext);
        }

        [Fact]
        private void BlogRepository_SelectAll()
        {
            var result = objRepository.GetAll().ToList();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Title1", result[0].Title);
            Assert.Equal("Title2", result[1].Title);
            Assert.Equal("Title3", result[2].Title);
        }

        [Fact]
        private void BlogRepository_SelectById()
        {
            var result = objRepository.SelectById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Title1", result.Title);
            Assert.Equal("Content1", result.Content);
        }

        [Fact]
        public void BlogRepository_Create()
        {
            var blogPost = new BlogPost { Title = "Title", Content = "Content" };

            var result = objRepository.Create(blogPost);
            databaseContext.SaveChanges();

            var lst = objRepository.GetAll().ToList();

            Assert.Equal(4, lst.Count);
            Assert.Equal(blogPost.Title, lst.Last().Title);
            Assert.Equal(blogPost.Content, lst.Last().Content);
        }

        [Fact]
        public void BlogRepository_Update()
        {
            var blogPost = new BlogPost {Id=1, Title = "Title1Edited", Content = "Content1Edited" };

            objRepository.Update(blogPost);

            Assert.Equal(databaseContext.Entry(blogPost).State, System.Data.Entity.EntityState.Modified);

            databaseContext.SaveChanges();

            var lst = objRepository.GetAll().ToList();

            Assert.Equal(1, lst[0].Id);
            Assert.Equal("Title1Edited", lst[0].Title);
            Assert.Equal("Content1Edited", lst[0].Content);
        }

        [Fact]
        public void BlogRepository_Delete()
        {
            var blogPost = new TestProject.Models.BlogPost {Title = "Title1", Content = "Content1" };

            var toDelete = objRepository.SelectById(1);
            var result = objRepository.Delete(toDelete);
            databaseContext.SaveChanges();

            var lst = objRepository.GetAll().ToList();

            Assert.Equal(2, lst.Count);
            Assert.DoesNotContain(blogPost, lst);
        }
    }
}