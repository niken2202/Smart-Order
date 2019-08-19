using Data.Infrastructure;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using Moq;
using Service;
using System.Collections.Generic;

namespace UnitTest.ServiceTest
{
    /// <summary>
    /// Summary description for BillServiceTest
    /// </summary>
    [TestClass]
    public class CartServiceTest
    {
        private Mock<ICartRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private ICartService _service;
        private List<Cart> _listCategory;
        Cart c;
        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<ICartRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _service = new CartService(_mockUnitOfWork.Object, _mockRepository.Object);
            c= new Cart()
            {
                ID = 1
            };
            _listCategory = new List<Cart>()
            {
                new Cart() {ID =2 , CartPrice=1, TableID=1},
                new Cart() {ID =1 , CartPrice=1, TableID=1},
             };
        }

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
            _mockRepository.Setup(m => m.Add(c)).Returns((Cart p) =>
            {
                p.ID = 1;
                return p;
            });
            var result = _service.Add(c);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void Cart_Repository_GetByTable()
        {
            _mockRepository.Setup(m => m.GetCartByTable(1)).Returns(c);
            var list = _service.GetCartByTable(1);
            Assert.AreEqual(1, list.ID);
        }

        [TestMethod]
        public void Cart_Repository_Delete()
        {
           
            _mockRepository.Setup(m => m.Delete(1)).Returns(c);
            var list = _service.Delete(1);
            Assert.AreEqual(1, list.ID);
        }
    }
}