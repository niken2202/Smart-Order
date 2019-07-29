using Data.Infrastructure;
using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface ICartDetailService
    {
        CartDetail Add(CartDetail cartDetail);
        void Update(CartDetail cartDetail);
        void DeleteMulti(int cartID);
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

        public void DeleteMulti(int cartID)
        {
            cartDetailRepository.DeleteMulti(x => x.CartID == cartID);
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