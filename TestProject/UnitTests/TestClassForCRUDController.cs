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

namespace TestProject.UnitTests
{
    public class TestClassForCRUDController
    {
        /*private CRUDController testController;
        private IBlogRepository fakeBlogList;

        public TestClassForCRUDController()
        {
            fakeBlogList = A.Fake<IBlogRepository>();
            testController = new CRUDController;
        }

        #region Tests for Default

        [Fact]
        private void CRUD_Index_Returns_List_Of_Entries()
        { 
            A.CallTo(() => fakeBlogList.SelectAll()).Returns(new List<BlogPost>{
                    new BlogPost {ID=1, Content="Test1", Title="Title1"},
                    new BlogPost {ID=2, Content="Test1", Title="Title2"},
                    new BlogPost {ID=3, Content="Test1", Title="Title3"},
                    new BlogPost {ID=4, Content="Test1", Title="Title4"},
            });

            var viewResult = testController.Index() as ViewResult;
            var fakePosts = viewResult.ViewData.Model as List<BlogPost>;

            Assert.NotNull(viewResult);
            Assert.Equal(1, fakePosts[0].ID);
            Assert.Equal("Test1", fakePosts[0].Content);
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
            A.CallTo(() => fakeBlogList.SelectByID(1)).Returns<BlogPost>(null);

            var fakeHttpCodeResult = testController.Details(1) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(404, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_Details_Returns_Details_Proper_ID()
        {
            A.CallTo(() => fakeBlogList.SelectByID(1)).Returns(
                new BlogPost { ID = 1, Content = "TestContent", Title = "TestTitle" });

            var viewResult = testController.Details(1) as ViewResult;
            var fakeBlog = viewResult.ViewData.Model as BlogPost;

            Assert.NotNull(viewResult);
            Assert.Equal(1, fakeBlog.ID);
            Assert.Equal("TestContent", fakeBlog.Content);
            Assert.Equal("TestTitle", fakeBlog.Title);
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
            A.CallTo(() => fakeBlogList.Create(A<BlogPost>._));
            var errPost = new BlogPost() { ID = 1 };

            A.CallTo(() => fakeBlogList.Create(errPost)).MustNotHaveHappened();
            A.CallTo(() => fakeBlogList.Save()).MustNotHaveHappened();

            testController.ModelState.AddModelError("Error", "Pole Title jest wymagane.");

            var viewResult = testController.Create(errPost) as ViewResult;

            Assert.NotNull(viewResult);
            Assert.Equal("Create", viewResult.ViewName);
        }

        [Fact]
        private void CRUD_Create_HTTPPost_Correct_Entry()
        {
            A.CallTo(() => fakeBlogList.Create(A<BlogPost>._));
            var correctPost = new BlogPost() { ID = 1, Content = "Content", Title = "Title" };

            A.CallTo(() => fakeBlogList.Create(correctPost)).MustNotHaveHappened();
            A.CallTo(() => fakeBlogList.Save()).MustNotHaveHappened();

            var viewResult = testController.Create(correctPost) as RedirectToRouteResult;

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

            A.CallTo(() => fakeBlogList.Update(null)).MustNotHaveHappened();
            A.CallTo(() => fakeBlogList.Save()).MustNotHaveHappened();

            testController.ModelState.AddModelError("Error", "Pole Title jest wymagane.");

            var viewResult = testController.HttpPostEdit(null) as ViewResult;

            Assert.NotNull(viewResult);
            Assert.Equal("Edit", viewResult.ViewName);
        }

        [Fact]
        private void CRUD_HttpPostEdit_HTTPPost_Correct_Entry()
        {
            A.CallTo(() => fakeBlogList.Update(A<BlogPost>._));
            var correctEdit = new BlogPost() { ID = 1, Content = "Content", Title = "Title" };

            A.CallTo(() => fakeBlogList.Create(correctEdit)).MustNotHaveHappened();
            A.CallTo(() => fakeBlogList.Save()).MustNotHaveHappened();

            var viewResult = testController.HttpPostEdit(correctEdit) as RedirectToRouteResult;

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
        private void CRUD_Delete_Returns_404_ID_Not_Found()
        {
            A.CallTo(() => fakeBlogList.SelectByID(1)).Returns<BlogPost>(null);

            var fakeHttpCodeResult = testController.Delete(1) as HttpStatusCodeResult;

            Assert.NotNull(fakeHttpCodeResult);
            Assert.Equal(404, fakeHttpCodeResult.StatusCode);
        }

        [Fact]
        private void CRUD_Delete_Proper_ID()
        {
            A.CallTo(() => fakeBlogList.SelectByID(1)).Returns(
            new BlogPost { ID = 1, Content = "TestContent", Title = "TestTitle" });

            A.CallTo(() => fakeBlogList.Delete(1)).MustNotHaveHappened();
            A.CallTo(() => fakeBlogList.Save()).MustNotHaveHappened();

            var viewResult = testController.ConfirmDelete(1) as RedirectToRouteResult;

            Assert.NotNull(viewResult);
            Assert.Equal("Index", viewResult.RouteValues["action"]);
        }

        #endregion*/
    }
}
