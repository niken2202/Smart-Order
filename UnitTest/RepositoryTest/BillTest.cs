using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for BillTest
    /// </summary>
    [TestClass]
    public class BillTest
    {
        private IDbFactory dbFactory;
        private IBillRepository billRepository;
        private IUnitOfWork unitOfWork;

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

        #endregion Additional test attributes

        [TestMethod]
        public void Add_Bill_Test()
        {
            Bill bill = new Bill();
            bill.CustomerName = "Test bill";
            bill.CreatedDate = DateTime.Now;
            bill.CreatedBy = "Test";
            bill.Content = "Trung rong hap sa";
            bill.Status = true;
            bill.BillDetail = new[]
            {
                new BillDetail()
                {
                    BillID = 1,
                 Image = "dsds",
                 Name = "Mon chua ngot",
                 Price = 10,
                 Description = "description",
                  Amount = 1
                 } ,
                new BillDetail()
                {
                    BillID = 1,
                 Image = "dsds",
                 Name = "Mon chua ngot 2",
                 Price = 10,
                 Description = "description 2",
                  Amount = 2
                 }
        };
        var result = billRepository.Add(bill);
        unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

    [TestMethod]
    public void Bill_Repository_GetAll()
    {
        var list = billRepository.GetAll().ToList();
        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetTimeRange()
    {
        var list = billRepository.GetTimeRange(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01)).ToList();
        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetBillLast7Days()
    {
        var list = billRepository.GetBillLast7Days().ToList();
        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetLastMonth()
    {
        var list = billRepository.GetBillLastMonth().ToList();
        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetBillToday()
    {
        var list = billRepository.GetBillToday().ToList();
        Assert.AreEqual(1, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetRevenue()
    {
        var list = billRepository.GetRevenueStatistic(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01)).ToList();
        Assert.AreEqual(0, list.Count);
    }

    [TestMethod]
    public void Bill_Repository_GetRevenueGroupByMonth()
    {
        var list = billRepository.GetRevenueStatistic(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01)).ToList();
        Assert.AreEqual(0, list.Count);
    }
}
}