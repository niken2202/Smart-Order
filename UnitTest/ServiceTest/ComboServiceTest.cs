using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System.Linq;
using System.Collections.Generic;
using Common.ViewModels;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class ComboServiceTest
    {
        private Mock<IComboRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IComboService _service;
        private List<Combo> _listCombo;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IComboRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ComboService( _mockUnitOfWork.Object, _mockRepository.Object);
          
            _listCombo = new List<Combo>()
            {
                new Combo()
                {
            Status=true,
            Image = "dsds",
            Name = "Mon chua ngot",
            Price = 10,
            Description = "description",
            Amount = 1,
                }
        };
        }
        [TestMethod]
        public void Combo_Service_Add()
        {
            Combo combo = new Combo();
            combo.Description = "Test combo";
            combo.Price = 10;
            combo.Name = "Test";
            combo.Amount = 10;
            combo.Status = true;
           
            _mockRepository.Setup(m => m.Add(combo)).Returns((Combo c) => {
                c.ID = 1;
                return c;
            });

            var result = _service.Add(combo);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Combo_Service_Update()
        {
            Combo combo = new Combo();
            combo.Description = "Test combo";
            combo.Price = 10;
            combo.Name = "Test";
            combo.Amount = 10;
            combo.Status = true;

            _mockRepository.Setup(m => m.Update(combo));

            _service.Update(combo);
        }

        [TestMethod]
        public void Combo_Service_GetComboById()
        {
            ComboViewModel cv = new ComboViewModel()
            {
                Status = true,
                Image = "dsds",
                Name = "Mon chua ngot",
                Price = 10,
                Description = "description",
                Amount = 1,
                ID = 1
            };
            //setup method
            _mockRepository.Setup(m => m.GetComboById(1)).Returns(cv);
            //call action
            var result = _service.GetCombobyId(1);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Combo_Service_Delete()
        {
            Combo b = new Combo()
            {
                Status = true,
                Image = "dsds",
                Name = "Mon chua ngot",
                Price = 10,
                Description = "description",
                Amount = 1,
                ID = 1
            };
            _mockRepository.Setup(m => m.Delete(1)).Returns(b);
            var result = _service.Delete(1);
            Assert.AreEqual(1, result.ID);
        }
    }
}