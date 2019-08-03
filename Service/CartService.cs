using Data.Infrastructure;
using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface ICartService
    {
        Cart Add(Cart cart);
        void Update(Cart cart);
        Cart Delete(int id);
        Cart GetCartByTable(int tableID);
        void SaveChanges();
    }

    public class CartService : ICartService
    {
        IUnitOfWork unitOfWork;
        ICartRepository cartRepository;

        public CartService(IUnitOfWork unitOfWork, ICartRepository cartRepository)
        {
            this.unitOfWork = unitOfWork;
            this.cartRepository = cartRepository;
        }

        public Cart Add(Cart cart)
        {
            return cartRepository.Add(cart);
        }

        public Cart Delete(int id)
        {
            return cartRepository.Delete(id);
        }

        public Cart GetCartByTable(int tableID)
        {
            return cartRepository.GetCartByTable(tableID);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(Cart cart)
        {
             cartRepository.Update(cart);
        }
    }
}