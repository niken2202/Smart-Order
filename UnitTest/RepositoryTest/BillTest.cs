using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for BillTest
    /// </summary>
    [TestClass]
    public class BillTest
    {
        IDbFactory dbFactory;
        IBillRepository billRepository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            billRepository = new BillRepository(dbFactory);
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
        #endregion

        [TestMethod]
        public void Bill_Create_Test()
        {
            Bill bill = new Bill();
            bill.CustomerName = "Test bill";
            bill.CreatedDate = DateTime.Now;
            bill.CreatedBy = "Test";
            bill.Content = "Trung rong hap sa";
            bill.Status = true;
            var result = billRepository.Add(bill);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.ID);
        }
        [TestMethod]
        public void Bill_Repository_GetAll()
        {
            var list = billRepository.GetAll().ToList();
            Assert.AreEqual(2, list.Count);
        }
    }
}
