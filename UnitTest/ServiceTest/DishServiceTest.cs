using Common.ViewModels;
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
    public class DishServiceTest
    {
        private Mock<IDishRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IDishService _service;
        private List<Dish> _listDish;
        private List<DishViewModel> _listDishModelView;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IDishRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new DishService(_mockUnitOfWork.Object, _mockRepository.Object);

            _listDish = new List<Dish>()
            {
                new Dish() {ID =1 , Name="fddf"},
                new Dish() {ID =2 }
             };
            _listDishModelView = new List<DishViewModel>()
            {
                new DishViewModel() {ID =1 , Name="fddf"},
                new DishViewModel() {ID =2 }
             };
        }

        [TestMethod]
        public void Dish_Service_Add()
        {
            Dish d = new Dish();
            d.CategoryID = 1;
            d.CreatedDate = DateTime.Now;
            d.Description = "unit test";
            d.Image = "unit test";
            d.OrderCount = 1;
            d.Price = 1;
            d.Status = 1;
            d.Amount = 1;
            _mockRepository.Setup(m => m.Add(d)).Returns((Dish p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _service.Add(d);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Dish_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll()).Returns(_listDishModelView);
            var result = _service.GetAll().ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Dish_Service_GetTopHot()
        {
            _mockRepository.Setup(m => m.GetTopHot()).Returns(_listDish);
            var result = _service.GetTopDish().ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Dish_Service_GetByCombo()
        {
            _mockRepository.Setup(m => m.GetDishByCombo(1)).Returns(_listDish);
            var result = _service.GetAllByComboId(1).ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Dish_Service_GetByID()
        {
            _mockRepository.Setup(m => m.GetSingleById(1)).Returns(_listDish[0]);
            var result = _service.GetById(1);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Dish_Service_GetDishByCategory()
        {
            _mockRepository.Setup(m => m.GetDishByCategory(1)).Returns(_listDish);
            var result = _service.GetByCategogy(1).ToList();
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void Dish_Service_Delete()
        {
            _mockRepository.Setup(m => m.Delete(1)).Returns(_listDish[0]);
            var result = _service.Delete(1);
            Assert.AreEqual(1, result.ID);
        }
    }
}