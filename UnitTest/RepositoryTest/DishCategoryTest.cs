using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for BillDetailTest
    /// </summary>
    [TestClass]
    public class DishCategoryTest
    {
        private IDbFactory dbFactory;
        private IDishCategoryRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new DishCategoryRepository(dbFactory);
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
        public void Add_DishCategory_Test()
        {
            DishCategory dc = new DishCategory();
            dc.Name = "Test";
            dc.CreatedDate = DateTime.Now;
            var result = _repository.Add(dc);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.ID);
        }

        [TestMethod]
        public void Cart_Repository_GetAll()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void DishCategory_Repository_Delete()
        {
            var list = _repository.Delete(1);
            Assert.AreEqual(1, list.ID);
        }
    }
}