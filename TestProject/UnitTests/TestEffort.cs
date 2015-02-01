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
    public class TestEffort
    {
        DbConnection connection;
        TestContext databaseContext;
        BlogRepository objRepository;

        public TestEffort()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            databaseContext = new TestContext(connection);
            objRepository = new BlogRepository((TestContext)databaseContext);
        }

        [Fact]
        private void BlogRepository_SelectAll()
        {
            var result = objRepository.SelectAll().ToList();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("Title1", result[0].Title);
            Assert.Equal("Title2", result[1].Title);
            Assert.Equal("Title3", result[2].Title);
        }
    }
}