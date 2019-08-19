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
    public class ApplicationUserTest
    {
        private IDbFactory dbFactory;
        private IApplicationUserRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new ApplicationUserRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void ApplicationUser_Add_Test()
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = "unitTest@gmail.com";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.TwoFactorEnabled = false;
            var result = _repository.Add(user);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual("unitTest@gmail.com", result.Email);
        }
    }
}
