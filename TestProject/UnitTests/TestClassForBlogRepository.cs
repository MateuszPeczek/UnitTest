using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Xunit;
using FakeItEasy;
using TestProject.Models;
using TestProject.Repository;

namespace TestProject.UnitTests
{
    public class TestClassForBlogRepository
    {
        private IBlogRepository fakeBlogList;

        public TestClassForBlogRepository()
        {
            fakeBlogList = A.Fake<IBlogRepository>();
        }
    }
}