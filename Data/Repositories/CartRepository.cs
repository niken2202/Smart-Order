using Data.Infrastructure;
using Model.Models;
using System.Linq;
namespace Data.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCartByTable(int tableID);
    }
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

       public override Cart Add(Cart cart)
        {
            cart.CartPrice = cart.CartDetails.Sum(x => (x.Quantity * x.Price));
            var c = DbContext.Cart.Add(cart);
            foreach(var cd in cart.CartDetails)
            {
                DbContext.CartDetail.Add(cd);
            }
            return c;
        }

        public Cart GetCartByTable(int tableID)
        {
            var cart = (from c in DbContext.Cart
                        where c.TableID == tableID
                        select c).SingleOrDefault();
            if (cart == null) return null;
            cart.CartDetails = (from cd in DbContext.CartDetail
                               where cd.CartID == cart.ID
                               select cd).ToList();
            return cart;
        }
    }
}