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
    public class HistoryServiceTest
    {
        private Mock<IHistoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IHistoryService _service;
        private List<History> _listCategory;
        History c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IHistoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new HistoryService(_mockUnitOfWork.Object, _mockRepository.Object);
            c = new History()
            {
                ID = 1
            };
            _listCategory = new List<History>()
            {
                new History() {ID =2 },
                new History() {ID =1},
             };
        }
        [TestMethod]
        public void Add_Cart_Test()
        {
            History h = new History();
            h.TaskName = "Unit Test";
            h.CreatedDate = DateTime.Now;
            h.ID = 1;
            _mockRepository.Setup(m => m.Add(h)).Returns(h);
            var result = _service.Add(h);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Cart_Repository_GetHistoryToday()
        {
            _mockRepository.Setup(m => m.GetHistoryToday()).Returns(_listCategory);
            var list = _service.GetHistoryToday().ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetHistoryLastMonth()
        {
            _mockRepository.Setup(m => m.GetHistoryLastMonth()).Returns(_listCategory);
            var list = _service.GetHistoryLastMonth().ToList();
            Assert.AreEqual(2, list.Count);
        }
        [TestMethod]
        public void Cart_Repository_GetHistoryLast7Days()
        {
            _mockRepository.Setup(m => m.GetHistoryLast7Days()).Returns(_listCategory);
            var list = _service.GetHistoryLast7Days().ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetAll()
        {
            _mockRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
            var list = _service.GetAll().ToList();
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void Cart_Repository_GetTimeRange()
        {
            _mockRepository.Setup(m => m.GetTimeRange(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01))).Returns(_listCategory);
            var list = _service.GetTimeRange(new DateTime(2018, 01, 01), new DateTime(2019, 01, 01)).ToList();
            Assert.AreEqual(2, list.Count);
        }

    }
}