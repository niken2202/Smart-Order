using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface ICartDetailService
    {
        CartDetail Add(CartDetail cartDetail);
        void Update(CartDetail cartDetail);
        IEnumerable<CartDetailViewModel> GetAll();
        void DeleteMulti(int cartID);

        CartDetail Delete(int id);
        void SaveChanges();
    }
    public class CartDetailService : ICartDetailService
    {
        IUnitOfWork unitOfWork;
        ICartDetailRepository cartDetailRepository;

        public CartDetailService(IUnitOfWork unitOfWork, ICartDetailRepository cartDetailRepository)
        {
            this.unitOfWork = unitOfWork;
            this.cartDetailRepository = cartDetailRepository;
        }

        public CartDetail Add(CartDetail cartDetail)
        {
            return cartDetailRepository.Add(cartDetail);
        }

        public CartDetail Delete(int id)
        {
            return cartDetailRepository.Delete(id);
        }

        public void DeleteMulti(int cartID)
        {
            cartDetailRepository.DeleteMulti(x => x.CartID == cartID);
        }

        public IEnumerable<CartDetailViewModel> GetAll()
        {
            return cartDetailRepository.GetAll();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(CartDetail cartDetail)
        {
            cartDetailRepository.Update(cartDetail);
        }
    }
}