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
    public  class DishCategoryServiceTest
    {
        private Mock<IDishCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IDishCategoryService _service;
        private List<DishCategory> _listCategory;
        
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IDishCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new DishCategoryService( _mockRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<DishCategory>()
            {
                new DishCategory() {ID =1,CreatedDate = DateTime.Now.Date, Name="fdfdfdfd"},
                 new DishCategory() {ID =2,CreatedDate = DateTime.Now.Date, Name="fdfdfdfd"},
                 new DishCategory() {ID =3,CreatedDate = DateTime.Now.Date, Name="fdfdfdfd"},
            };
           
        }
        [TestMethod]
        public void DishCategory_Service_Create()
        {
            _mockRepository.Setup(m => m.Add(_listCategory[0])).Returns((DishCategory p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _service.Add(_listCategory[0]);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Cart_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
            var list = _service.GetAll().ToList();
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void DishCategory_Service_Delete()
        {
            _mockRepository.Setup(m => m.Delete(1)).Returns(_listCategory[0]);
            var list = _service.Delete(1);
            Assert.AreEqual(1, list.ID);
        }
    }

}