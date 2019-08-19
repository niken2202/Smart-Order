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
    public class ApplicationUserRoleTest
    {
        private IDbFactory dbFactory;
        private IApplicationUserRoleRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new ApplicationUserRoleRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void ApplicationUserRole_Add_Test()
        {
            ApplicationUserRole aur = new ApplicationUserRole();
            aur.RoleId = "694b17b4-020d-42ed-b00d-f035c703c3ae";
            aur.UserId = "91168ceb-22ca-47a4-bc2a-de852c4734ff";
             var result = _repository.Add(aur);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual("694b17b4-020d-42ed-b00d-f035c703c3ae", result.RoleId);
        }
    }
}
