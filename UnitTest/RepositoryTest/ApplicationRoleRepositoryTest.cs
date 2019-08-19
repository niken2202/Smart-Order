using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    [TestClass]
    public class ApplicationRoleRepositoryTest
    {
        private IDbFactory dbFactory;
        private IApplicationRoleRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new ApplicationRoleRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }
        [TestMethod]
        public void Add_ApplicationRole_Test()
        {
            ApplicationRole r = new ApplicationRole();
            r.Description = "Unit Test";
            var result = _repository.Add(r);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual("Unit Test", result.Description);
        }
        [TestMethod]
        public void GetAll_ApplicationRole_Test()
        {
            var result = _repository.GetAll().ToList();
            Assert.AreEqual(1, result.Count);
        }
    }
}