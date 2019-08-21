using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class PromotionCodeTest
    {
        private IDbFactory dbFactory;
        private IPromotionCodeRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new PromotionCodeRepository(dbFactory);
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
        public void ProCode_Add_Test()
        {
            PromotionCode c = new PromotionCode();
            c.CreatedDate = DateTime.Now;
            c.ExpiredDate = DateTime.Now.AddDays(30);
            c.Status = true;
            c.Code = "D";
            var result = _repository.Add(c);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual("D", result.Code);
        }

        [TestMethod]
        public void ProCode_Repository_GetByCode()
        {
            var list = _repository.GetSingleByCondition(x => x.Code=="D");
            Assert.AreEqual("D", list.Code);
        }
        [TestMethod]
        public void ProCode_Repository_GetAll()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(1, list.Count);
        }
        [TestMethod]
        public void ProCode_Repository_Delete()
        {
            var list = _repository.Delete(1);
            Assert.AreEqual("D", list.Code);
        }

    }
}