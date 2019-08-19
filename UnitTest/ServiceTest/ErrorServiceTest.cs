using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class ErrorServiceTest
    {
        private Mock<IErrorRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IErrorService _service;
        private List<Error> _listError;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IErrorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ErrorService( _mockRepository.Object, _mockUnitOfWork.Object);

            _listError = new List<Error>()
            {
                new Error() {ID =2 },
                new Error() {ID =1 },
             };
        }

        [TestMethod]
        public void Error_Add_Test()
        {
            Error c = new Error();
            c.Message = "unit test";
            c.StackTrace = "unit test";
            c.CreatedDate = DateTime.Now;
            c.ID = 1;
            _mockRepository.Setup(m => m.Add(c)).Returns(c);
            var result = _service.Create(c);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}