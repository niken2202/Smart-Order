using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System.Linq;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class BillDetailServiceTest
    {
        private Mock<IBillDetailRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IBillDetailService _service;
        private List<BillDetail> _listBillDetail;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IBillDetailRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new BillDetailService(_mockRepository.Object, _mockUnitOfWork.Object);
            _listBillDetail = new List<BillDetail>()
            {
                new BillDetail()
                {
                    ID=1,
            BillID = 1,
            Image = "dsds",
            Name = "Mon chua ngot",
            Price = 10,
            Description = "description",
            Amount = 1,
                }
        };
        }

        [TestMethod]
        public void BillDetail_Service_GetByBillID()
        {
            //setup method
            _mockRepository.Setup(m => m.GetByBillID(1)).Returns(_listBillDetail);
            //call action
            var result = _service.GetByBillId(1) as List<BillDetail>;
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void BillDetail_Service_Add()
        {
            //setup method
            _mockRepository.Setup(m => m.Add(_listBillDetail[0]));
            //call action
            var result = _service.Add(_listBillDetail[0]);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}