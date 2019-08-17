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
    /// Summary description for DishTest
    /// </summary>
    [TestClass]
    public class DishTest
    {
        private IDbFactory dbFactory;
        private IDishRepository _repository;
        private IUnitOfWork unitOfWork;
        public DishTest()
        {
            dbFactory = new DbFactory();
            _repository = new DishRepository(dbFactory);
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
        public void Dish_Repository_Add()
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
            var result = _repository.Add(d);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Dish_Repository_GetAll()
        {
            var result = _repository.GetAll().ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Dish_Repository_GetTopHot()
        {
            var result = _repository.GetTopHot().ToList();
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void Dish_Repository_GetByCombo()
        {
            var result = _repository.GetDishByCombo(1).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Dish_Repository_GetByID()
        {
            var result = _repository.GetSingleById(1);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Dish_Repository_GetDishByCategory()
        {
            var result = _repository.GetDishByCategory(1).ToList();
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Dish_Repository_Delete()
        {
            var result = _repository.Delete(1);
            Assert.AreEqual(1, result.ID);
        }
    }
}
