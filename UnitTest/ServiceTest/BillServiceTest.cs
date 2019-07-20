
using System;
using System.Collections.Generic;
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
        private IBillService _categoryService;
        private List<Bill> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IBillRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new BillService( _mockUnitOfWork.Object, _mockRepository.Object);
            _listCategory = new List<Bill>()
            {
                new Bill() {ID =3 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true},
                new Bill() {ID =4 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true },
                new Bill() {ID =5 ,CustomerName = "Test bill",CreatedDate = DateTime.Now,CreatedBy = "Test",Content = "Trung rong hap sa",Status = true },
            };
        }

        [TestMethod]
        public void Bill_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);

            //call action
            var result = _categoryService.GetAll() as List<Bill>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
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

            var result = _categoryService.Add(bill);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);


        }



    }
}
