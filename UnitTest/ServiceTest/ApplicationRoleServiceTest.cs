using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ServiceTest
{
    [TestClass]
   public class ApplicationRoleServiceTest
    {
        private Mock<IApplicationRoleRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IApplicationRoleService _service;
        private List<ApplicationRole> _listCategory;
        ApplicationRole c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IApplicationRoleRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new ApplicationRoleService(_mockUnitOfWork.Object, _mockRepository.Object);
            c = new ApplicationRole()
            {
                Id = "121-21",
            };
        }

        [TestMethod]
        public void Add_ApplicationRole_Test()
        {
            _mockRepository.Setup(m => m.Add(c)).Returns(c);
            var result = _service.Add(c);
            Assert.IsNotNull(result);
            Assert.AreEqual("121-21", result.Id);
        }
    }
}
