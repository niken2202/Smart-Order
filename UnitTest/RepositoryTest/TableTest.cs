using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Linq;

namespace UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for BillTest
    /// </summary>
    [TestClass]
    public class TableTest
    {
        IDbFactory dbFactory;
        ITableRepository _repository;
        IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new TableRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Add_Table_Test()
        {
            Table table = new Table();
            table.Name = "Ban 1";
            table.Status = 1;
            table.DeviceID = "dsdsdsd";
            table.CreatedDate = DateTime.Now;
            var result = _repository.Add(table);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.ID);
        }
        [TestMethod]
        public void Table_Repository_GetAll()
        {
            var list = _repository.GetAll().ToList();
            Assert.AreEqual(5, list.Count);
        }
        [TestMethod]
        public void Table_Repository_GetVairable()
        {
            var result = _repository.GetVariableTable().ToList();
            Assert.AreEqual(0, result.Count);
        }
        [TestMethod]
        public void Table_Repository_Delete()
        {
            var result = _repository.Delete(1);
            Assert.AreEqual(1, result.ID);
        }


    }
}
