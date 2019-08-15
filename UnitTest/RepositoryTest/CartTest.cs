using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;

namespace UnitTest.RepositoryTest
{
    /// <summary>
    /// Summary description for BillDetailTest
    /// </summary>
    [TestClass]
    public class CartTest
    {
        private IDbFactory dbFactory;
        private ICartRepository _repository;
        private IUnitOfWork unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            _repository = new CartRepository(dbFactory);
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

        #endregion Additional test attributes

        [TestMethod]
        public void Add_Cart_Test()
        {
            Cart c = new Cart();
            c.TableID = 1;
            c.CartPrice = 30;
            c.CartDetails = new[] {
                new CartDetail()
                {
                     Image= "image",
                     Note = "note",
                     Name= "thi",
                     Price= 10,
                     ProID=1,
                     Quantity=2,
                     Status=1,
                     Type=1,
                },
                new CartDetail()
                {
                     Image= "image2",
                     Note = "note2",
                     Name= "thit 2",
                     Price= 10,
                     ProID=1,
                     Quantity=3,
                     Status=1,
                     Type=1,
                }
            };
            var result = _repository.Add(c);
            unitOfWork.Commit();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Cart_Repository_GetByTable()
        {
            var list = _repository.GetCartByTable(1);
            Assert.AreEqual(1, list.ID);
        }

        [TestMethod]
        public void Cart_Repository_Delete()
        {
            var list = _repository.Delete(1);
            Assert.AreEqual(1, list.ID);
        }
    }
}