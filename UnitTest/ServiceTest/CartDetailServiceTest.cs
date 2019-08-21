using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class CartDetailServiceTest
    {
        private Mock<ICartDetailRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ICartDetailService _service;
        private List<CartDetailViewModel> _listBillDetail;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ICartDetailRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new CartDetailService( _mockUnitOfWork.Object, _mockRepository.Object);
            _listBillDetail = new List<CartDetailViewModel>()
            {
                new CartDetailViewModel()
                {
            CartID = 1,
            Image = "dsds",
            Name = "Mon chua ngot",
            Price = 10,
            Note = "description",
             Quantity = 1,
                }
        };
        }

        [TestMethod]
        public void BillDetail_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll()).Returns(_listBillDetail);
            //call action
            var result = _service.GetAll() as List<CartDetailViewModel>;
            Assert.AreEqual(1, result.Count);
        }
  
    }
}