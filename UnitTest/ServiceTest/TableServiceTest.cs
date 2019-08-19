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
    public class TableServiceTest
    {
        private Mock<ITableRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ITableService _service;
        private List<Table> _listCategory;
        Table c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ITableRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new TableService(_mockUnitOfWork.Object, _mockRepository.Object);
            c = new Table()
            {
                ID = 1
            };
            _listCategory = new List<Table>()
            {
                new Table() {ID =2  },
                new Table() {ID =3 },
             };
        }
        [TestMethod]
        public void Add_Table_Test()
        {
            Table table = new Table();
            table.Name = "Ban 1";
            table.Status = 1;
            table.DeviceID = "dsdsdsd";
            table.CreatedDate = DateTime.Now;
            table.ID = 1;
            _mockRepository.Setup(m => m.Add(table)).Returns(table);
            var result = _service.Add(table);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
        [TestMethod]
        public void Table_Service_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
            var list = _service.GetAll().ToList();
            Assert.AreEqual(2, list.Count);
        }
        [TestMethod]
        public void Table_Service_GetVairable()
        {
            _mockRepository.Setup(m => m.GetVariableTable()).Returns(_listCategory);
            var result = _service.GetVariableTable().ToList();
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public void Table_Service_Delete()
        {
            _mockRepository.Setup(m => m.Delete(1)).Returns(_listCategory[0]);
            var result = _service.Delete(1);
            Assert.AreEqual(2, result.ID);
        }
    }
}
