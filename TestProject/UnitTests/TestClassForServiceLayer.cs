using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.Models;
using TestProject.Repository;
using TestProject.ServiceLayer;
using TestProject.UOW;
using Xunit;

namespace TestProject.UnitTests
{
    public class TestClassForServiceLayer
    {
        private IBlogRepository fakeRepository = A.Fake<IBlogRepository>();
        private IBlogPostService service;
        IUnitOfWork fakeUnitOfWork = A.Fake<IUnitOfWork>();
        List<BlogPost> blogList;

        public TestClassForServiceLayer()
        {
            service = new BlogPostService(fakeUnitOfWork, fakeRepository);
            blogList = new List<BlogPost> {
                new BlogPost {Id = 1, Title="Title1", Content="Content1"},
                new BlogPost {Id = 2, Title="Title2", Content="Content2"},
                new BlogPost {Id = 3, Title="Title3", Content="Content3"},
                new BlogPost {Id = 4, Title="Title4", Content="Content4"},
            };
        }

        [Fact]
        public void BlogPostService_GetAll()
        {
            A.CallTo(() => fakeRepository.GetAll()).Returns(blogList);

            var result = service.SelectAll() as List<BlogPost>;

            A.CallTo(() => fakeRepository.GetAll()).MustHaveHappened(Repeated.Exactly.Once);

            Assert.NotNull(result);
            Assert.Equal(blogList.Count, result.Count);
        }

        [Fact]
        private void BlogPostService_GetById()
        {
            A.CallTo(() => fakeRepository.SelectById(1)).Returns(blogList[0]);

            var result = service.GetById(1);

            A.CallTo(() => fakeRepository.SelectById(1)).MustHaveHappened(Repeated.Exactly.Once);

            Assert.NotNull(result);
            Assert.Equal(blogList[0].Id, result.Id);
            Assert.Equal(blogList[0].Content, result.Content);
            Assert.Equal(blogList[0].Title, result.Title);
        }

        [Fact]
        private void BlogPostService_Create_Null_Id()
        {
            A.CallTo(() => fakeRepository.Create(null)).Throws(() => new ArgumentNullException("entity"));

            A.CallTo(() => fakeRepository.Create(null)).MustNotHaveHappened();
            A.CallTo(() => fakeUnitOfWork.Commit()).MustNotHaveHappened();
        }

        [Fact]
        private void BlogPostService_Create_Proper_Id()
        {
            var blogPost = blogList[0];

            service.Create(blogPost);

            A.CallTo(() => fakeRepository.Create(blogPost)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeUnitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        private void BlogPostService_Edit_Null_Id()
        {
            A.CallTo(() => fakeRepository.Update(null)).Throws(() => new ArgumentNullException("entity"));

            A.CallTo(() => fakeRepository.Update(null)).MustNotHaveHappened();
            A.CallTo(() => fakeUnitOfWork.Commit()).MustNotHaveHappened();
        }

        [Fact]
        private void BlogPostService_Edit_Proper_Id()
        {
            var blogPost = blogList[0];

            service.Update(blogPost);

            A.CallTo(() => fakeRepository.Update(blogPost)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeUnitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Fact]
        private void BlogPostService_Delete_Null_Id()
        {
            A.CallTo(() => fakeRepository.Delete(null)).Throws(() => new ArgumentNullException("entity"));

            A.CallTo(() => fakeRepository.Delete(null)).MustNotHaveHappened();
            A.CallTo(() => fakeUnitOfWork.Commit()).MustNotHaveHappened();
        }

        [Fact]
        private void BlogPostService_Delete_Proper_Id()
        {
            var blogPost = blogList[0];

            service.Delete(blogPost);

            A.CallTo(() => fakeRepository.Delete(blogPost)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fakeUnitOfWork.Commit()).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}