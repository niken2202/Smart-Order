using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class HistoryTest
    {
        private IDbFactory dbFactory;
        private IHistoryRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new HistoryRepository(dbFactory);
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
        public void Add_Cart_Test()
        {
            History h = new History();
            h.TaskName = "Unit Test";
            h.CreatedDate = DateTime.Now;
            var result = _repository.Add(h);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ID);
        }

        [TestMethod]
        public void Cart_Repository_GetHistoryToday()
        {
            var list = _repository.GetHistoryToday().ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetHistoryLastMonth()
        {
            var list = _repository.GetHistoryLastMonth().ToList();
            Assert.AreEqual(3, list.Count);
        }
        [TestMethod]
        public void Cart_Repository_GetHistoryLast7Days()
        {
            var list = _repository.GetHistoryLast7Days().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetAll()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetTimeRange()
        {
            var list = _repository.GetTimeRange(new DateTime(2018,01,01), new DateTime(2019, 01, 01)).ToList();
            Assert.AreEqual(0, list.Count);
        }
    }
}