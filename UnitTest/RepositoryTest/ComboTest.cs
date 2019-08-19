using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class ComboTest
    {
        private IDbFactory dbFactory;
        private IComboRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new ComboRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void Combo_Repository_Add()
        {
            Combo combo = new Combo();
            combo.Description = "Test combo";
            combo.Price = 10;
            combo.Name = "Test";
            combo.Amount = 10;
            combo.Status = true;
            combo.DishComboMappings = new List<DishComboMapping>()
            {
                new DishComboMapping(){ DishID=2, Amount=1 }
            };
            var result = _repository.Add(combo);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.ID);
        }
        [TestMethod]
        public void Combo_Repository_GetAll()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Combo_Repository_GetbyId()
        {
            var list = _repository.GetComboById(1);
            Assert.AreEqual(1, list.ID);
        }

        [TestMethod]
        public void Combo_Repository_Delete()
        {
            var list = _repository.Delete(1);
            Assert.AreEqual(1, list.ID);
        }
    }
}