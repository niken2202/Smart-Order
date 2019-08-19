using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class MaterialTest
    {
        private IDbFactory dbFactory;
        private IMaterialRepository _repository;
        private IUnitOfWork unitOfWork;
        public MaterialTest()
        {
            dbFactory = new DbFactory();
            _repository = new MaterialRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void Material_Repository_Add()
        {
            Material m = new Material();
            m.CreatedDate = DateTime.Now;
            m.Price = 1;
            m.Amount = 1;
            m.Name = "dsds";
            var result = _repository.Add(m);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Material_Repository_GetAll()
        {
            var result = _repository.GetAll().ToList();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Material_Repository_GetByDish()
        {
            var result = _repository.GetAllByDishID(1).ToList();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void material_Repository_GetByID()
        {
            var result = _repository.GetSingleById(1);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Material_Repository_Delete()
        {
            var result = _repository.Delete(1);
            Assert.AreEqual(1, result.ID);
        }
    }
}