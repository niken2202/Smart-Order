using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class ApplicationUserRoleServiceTest
    {
        private Mock<IApplicationUserRoleRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IApplicationUserRoleService _service;
        private List<ApplicationUserRole> _listCategory;
        private ApplicationUserRole c;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IApplicationUserRoleRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ApplicationUserRoleService(_mockUnitOfWork.Object, _mockRepository.Object);
            c = new ApplicationUserRole()
            {
                UserId = "unitTest"
            };
        }

        [TestMethod]
        public void Add_UserRole_Service_Test()
        {
            _mockRepository.Setup(m => m.Add(c)).Returns(c);
            var result = _service.Add(c);
            Assert.AreEqual("unitTest", result.UserId);
        }
    }
}