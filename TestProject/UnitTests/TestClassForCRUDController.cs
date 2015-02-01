using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;
using FakeItEasy;
using TestProject.Repository;
using TestProject.Controllers;
using TestProject.Models;
using System.Web.Mvc;
using TestProject.ServiceLayer;

namespace TestProject.UnitTests
{
    public class TestClassForCRUDController
    {
        private CRUDController testController;
        IBlogPostService fakeBlogService = A.Fake<IBlogPostService>();
        private List<BlogPost> listBlogs;

        public TestClassForCRUDController()
        {
            testController = new CRUDController(fakeBlogService);
            listBlogs = new List<BlogPost>()
            {
                new BlogPost {Id = 1, Title="Title1", Content="Content1"},
                new BlogPost {Id = 2, Title="Title2", Content="Content2"},
                new BlogPost {Id = 3, Title="Title3", Content="Content3"},
                new BlogPost {Id = 4, Title="Title4", Content="Content4"},
            };
        }

        #region Tests for Default

        [Fact]
        private void CRUD_Index_Returns_List_Of_Entries()
        { 
            A.CallTo(() => fakeBlogService.SelectAll()).Returns(listBlogs);

            var viewResult = testController.Index() as ViewResult;
            var fakePosts = viewResult.ViewData.Model as List<BlogPost>;

            Assert.NotNull(viewResult);
            Assert.Equal(1, fakePosts[0].Id);
            Assert.Equal("Content1", fakePosts[0].Content);
            Assert.Equal("Title1", fakePosts[0].Title);
            Assert.Equal(4, fakePosts.Count);
        }

        #endregion

        #region Tests for Retrieve Action
        
        [Fact]
        private void CRUD_Details_Returns_400_Id_Null()
        {
            var fakeHttpCodeResult = testController.Details(null) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(400, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_Details_Returns_404_ID_Not_Found()
        {
            A.CallTo(() => fakeBlogService.GetById(1)).Returns<BlogPost>(null);

            var fakeHttpCodeResult = testController.Details(1) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(404, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_Details_Returns_Details_Proper_ID()
        {
            A.CallTo(() => fakeBlogService.GetById(1)).Returns(listBlogs[0]);

            var viewResult = testController.Details(1) as ViewResult;
            var fakeBlog = viewResult.ViewData.Model as BlogPost;

            Assert.NotNull(viewResult);
            Assert.Equal(listBlogs[0].Id, fakeBlog.Id);
            Assert.Equal(listBlogs[0].Content, fakeBlog.Content);
            Assert.Equal(listBlogs[0].Title, fakeBlog.Title);
        }

        #endregion

        #region Tests for Create()

        [Fact]
        private void CRUD_Create_HTTPGet_Returns_Index_View()
        {
            var viewResult = testController.Create() as ViewResult;

            Assert.NotNull(viewResult);
            Assert.Equal("Create", viewResult.ViewName);
        }

        [Fact]
        private void CRUD_Create_HTTPPost_Error_Incorrect_Entry()
        {
            var blogPost = new BlogPost {Id = 1};

            testController.ModelState.AddModelError("Error", "Title required");
            testController.ModelState.AddModelError("Error", "Content required");

            var viewResult = testController.Create(blogPost) as ViewResult;

            A.CallTo(() => fakeBlogService.Create(blogPost)).MustNotHaveHappened();

            Assert.NotNull(viewResult);
            Assert.Equal("Create", viewResult.ViewName);
        }

        [Fact]
        private void CRUD_Create_HTTPPost_Correct_Entry()
        {
            var blogPost = new BlogPost {Id=1, Content="Content1", Title="Title1"};

            var viewResult = testController.Create(blogPost) as RedirectToRouteResult;

            A.CallTo(() => fakeBlogService.Create(blogPost)).MustHaveHappened(Repeated.Exactly.Once);
            Assert.NotNull(viewResult);
            Assert.Equal("Index", viewResult.RouteValues["action"]);
        }

        #endregion

        #region Tests for Update Action

        [Fact]
        private void CRUD_Update_HTTPGet_Null_ID()
        {
            var fakeHttpCodeResult = testController.Edit(null) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(400, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_HttpPostEdit_HTTPPost_Error_Incorrect_Entry()
        {
            var blogPost = new BlogPost { Id = 1 };

            testController.ModelState.AddModelError("Error", "Title required");
            testController.ModelState.AddModelError("Error", "Content required");

            var viewResult = testController.HttpPostEdit(blogPost) as ViewResult;

            A.CallTo(() => fakeBlogService.Update(blogPost)).MustNotHaveHappened();

            Assert.NotNull(viewResult);
            Assert.Equal("Edit", viewResult.ViewName);
        }

        [Fact]
        private void CRUD_HttpPostEdit_HTTPPost_Correct_Entry()
        {
            var blogPost = new BlogPost() { Id = 1, Content = "Content", Title = "Title" };

            var viewResult = testController.HttpPostEdit(blogPost) as RedirectToRouteResult;

            A.CallTo(() => fakeBlogService.Update(blogPost)).MustHaveHappened(Repeated.Exactly.Once);

            Assert.NotNull(viewResult);
            Assert.Equal("Index", viewResult.RouteValues["action"]);
        }

        #endregion

        #region Tests for Delete Action

        [Fact]
        private void CRUD_Delete_HttpGet_Null_ID()
        {
            var fakeHttpCodeResult = testController.Delete(null) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(400, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_Delete_HTTPGET_Returns_404_ID_Not_Found()
        {
            A.CallTo(() => fakeBlogService.GetById(1)).Returns<BlogPost>(null);

            var fakeHttpCodeResult = testController.Delete(1) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(404, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_HTTPGet_Delete_Proper_ID()
        {
            A.CallTo(() => fakeBlogService.GetById(1)).Returns(listBlogs[0]);

            var viewResult = testController.Delete(1) as ViewResult;
            var fakeBlog = viewResult.ViewData.Model as BlogPost;

            Assert.NotNull(viewResult);
            Assert.Equal(listBlogs[0].Id, fakeBlog.Id);
            Assert.Equal(listBlogs[0].Content, fakeBlog.Content);
            Assert.Equal(listBlogs[0].Title, fakeBlog.Title);
        }

        [Fact]
        private void CRUD_HTTPPost_ConfirmDelete_Proper_ID()
        {
            A.CallTo(() => fakeBlogService.GetById(1)).Returns(listBlogs[0]);
            
            var blogPost = listBlogs[0];
            var data = new System.Web.Mvc.FormCollection();
            
            var viewResult = testController.ConfirmDelete(1, data)  as RedirectToRouteResult;

            A.CallTo(() => fakeBlogService.Delete(blogPost)).MustHaveHappened(Repeated.Exactly.Once);

            Assert.NotNull(viewResult);
            Assert.Equal("Index", viewResult.RouteValues["action"]);
        }
        
        #endregion 
    }
}
