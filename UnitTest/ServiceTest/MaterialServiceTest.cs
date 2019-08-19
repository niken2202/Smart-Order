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
    [TestClass]
    public class MaterialServiceTest
    {
        private Mock<IMaterialRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMaterialService _service;
        private List<Material> _listCategory;
        Material c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IMaterialRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new MaterialService(_mockUnitOfWork.Object, _mockRepository.Object);
          
            _listCategory = new List<Material>()
            {
                new Material() {ID =1 },
                new Material() {ID =2 },
             };
        }
        [TestMethod]
        public void Material_Service_Add()
        {
            Material m = new Material();
            m.CreatedDate = DateTime.Now;
            m.Price = 1;
            m.Amount = 1;
            m.ID = 1;
            _mockRepository.Setup(p => p.Add(m)).Returns(m);
            var result = _service.Add(m);
          
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Material_Service_GetAll()
        {
            _mockRepository.Setup(p => p.GetAll()).Returns(_listCategory);
            var result = _service.GetAll().ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Material_Service_GetByDish()
        {
            _mockRepository.Setup(p => p.GetAllByDishID(1)).Returns(_listCategory);
            var result = _service.GetAllByDishID(1).ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void material_Service_GetByID()
        {
            _mockRepository.Setup(p => p.GetSingleById(1)).Returns(_listCategory[0]);
            var result = _service.GetById(1);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Dish_Service_Delete()
        {
            _mockRepository.Setup(p => p.Delete(1)).Returns(_listCategory[0]);
            var result = _service.Delete(1);
            Assert.AreEqual(1, result.ID);
        }
    }
}
