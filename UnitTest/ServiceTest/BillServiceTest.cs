
using System;
using System.Collections.Generic;
using System.Linq;
using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;

namespace UnitTest.ServiceTest
{
    /// <summary>
    /// Summary description for BillServiceTest
    /// </summary>
    [TestClass]
    public class BillServiceTest
    {
        private Mock<IBillRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IBillService _service;
        private List<Bill> _listCategory;
        List<RevenueStatisticViewModel> _listRevenue;
        List<RevenueByMonthViewModel> _listRevenue2;
        List<BillViewModel> _listBVM;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IBillRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new BillService( _mockUnitOfWork.Object, _mockRepository.Object);
            _listCategory = new List<Bill>()
            {
                new Bill() {ID =3 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true, Discount=0, TableID=1, Total=10},
                new Bill() {ID =4 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true, Discount=0, TableID=1, Total=10 },
                new Bill() {ID =5 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true, Discount=0, TableID=1, Total=10 },
            };
            _listRevenue = new List<RevenueStatisticViewModel>()
            {
                new RevenueStatisticViewModel(){ Revenue=30,Date=DateTime.Now}
            };
            _listRevenue2 = new List<RevenueByMonthViewModel>()
            {
                new RevenueByMonthViewModel(){ Revenue=30,Month=8,Year=2019}
            };
            _listBVM = new List<BillViewModel>() { new BillViewModel { ID = 1 } };
        }
        [TestMethod]
        public void Bill_Service_Create()
        {
            Bill bill = new Bill();
            bill.CustomerName = "Test bill";
            bill.CreatedDate = DateTime.Now;
            bill.CreatedBy = "Test";
            bill.Content = "Trung rong hap sa";
            bill.Status = true;
            _mockRepository.Setup(m => m.Add(bill)).Returns((Bill p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _service.Add(bill);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Bill_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll()).Returns(_listBVM);

            //call action
            var result = _service.GetAll() as List<BillViewModel>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Bill_Service_GetTimeRange()
        {
            _mockRepository.Setup(m => m.GetTimeRange(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01))).Returns(_listCategory);

            var list = _service.GetTimeRange(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01)).ToList();
            Assert.AreEqual(3, list.Count);
        }
        [TestMethod]
        public void Bill_Service_GetBillLast7Days()
        {
            _mockRepository.Setup(m => m.GetBillLast7Days()).Returns(_listCategory);
            var list = _service.GetBillLast7Days().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Bill_Service_GetLastMonth()
        {
            _mockRepository.Setup(m => m.GetBillLastMonth()).Returns(_listCategory);
            var list = _service.GetBillLastMonth().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Bill_Service_GetBillToday()
        {
            _mockRepository.Setup(m => m.GetBillToday()).Returns(_listCategory);
            var list = _service.GetBillToday().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void Bill_Service_GetRevenue()
        {
            _mockRepository.Setup(m => m.GetRevenueStatistic(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01))).Returns(_listRevenue);
            var list = _service.GetRevenueStatistic(new DateTime(2018, 01, 01), new DateTime(2019, 09, 01)).ToList();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Bill_Service_GetRevenueGroupByMonth()
        {
            _mockRepository.Setup(m => m.GetRevenueGroupByMonth(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01))).Returns(_listRevenue2);
            var list = _service.GetRevenueGroupByMonth(new DateTime(2018, 01, 01), new DateTime(2019, 09, 01)).ToList();
            Assert.AreEqual(0, list.Count);
        }
       



    }
}
