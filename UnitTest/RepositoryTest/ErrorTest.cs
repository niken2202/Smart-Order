using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class ErrorTest
    {
        private IDbFactory dbFactory;
        private IErrorRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new ErrorRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion Additional test attributes

        [TestMethod]
        public void Error_Add_Test()
        {
            Error c = new Error();
            c.Message = "unit test";
            c.StackTrace = "unit test";
            c.CreatedDate = DateTime.Now;
            var result = _repository.Add(c);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}