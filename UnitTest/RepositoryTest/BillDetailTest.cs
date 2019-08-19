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
    public class BillDetailTest
    {
        private IDbFactory dbFactory;
        private IBillDetailRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new BillDetailRepository(dbFactory);
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
        public void Add_BillDetail_Test()
        {
            BillDetail bd = new BillDetail();
            bd.BillID = 1;
            bd.Image = "dsds";
            bd.Name = "Mon chua ngot";
            bd.Price = 10;
            bd.Description = "description";
            bd.Amount = 1;
            var result = _repository.Add(bd);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
           
        }

        [TestMethod]
        public void BillDetail_Repository_GetByBillID()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(13, list.Count);
        }

     
    }
}