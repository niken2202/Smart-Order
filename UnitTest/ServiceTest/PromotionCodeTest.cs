using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.ServiceTest
{
    public class PromotionCodeTest
    {
        private Mock<IPromotionCodeRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPromotionCodeService _service;
        private List<PromotionCode> _listCategory;
        PromotionCode c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPromotionCodeRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new PromotionCodeService(_mockUnitOfWork.Object, _mockRepository.Object);
            c = new PromotionCode()
            {
                Code = "C"
            };
            _listCategory = new List<PromotionCode>()
            {
                new PromotionCode() {Code ="A" },
                new PromotionCode() {Code ="B"},
             };
        }
        [TestMethod]
        public void ProCode_Add_Test()
        {
            PromotionCode c = new PromotionCode();
            c.CreatedDate = DateTime.Now;
            c.ExpiredDate = DateTime.Now.AddDays(30);
            c.Status = true;
            c.Code = "D";
            _mockRepository.Setup(m => m.Add(c)).Returns(c);
            var result = _service.Add(c);
          
            Assert.IsNotNull(result);
            Assert.AreEqual("D", result.Code);
        }

 
        [TestMethod]
        public void ProCode_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
            var list = _service.GetAll().ToList();
            Assert.AreEqual(2, list.Count);
        }
        [TestMethod]
        public void ProCode_Service_CheckValid()
        {
          //  _mockRepository.Setup(m => m.Ch(null)).Returns(_listCategory);
            var list = _service.CheckValid("D");
            Assert.AreEqual("D", list.Code);
        }
    }
}
