using Data.Infrastructure;
using Model.Models;
using System.Linq;
namespace Data.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCartByTable(int tableID);
        void ChangeTable(Cart cart);


    }
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public override Cart Add(Cart cart)
        {
            cart.CartPrice = cart.CartDetails.Sum(x => (x.Quantity * x.Price));
            Cart c = DbContext.Cart.SingleOrDefault(m => m.TableID == cart.TableID); ;
            if (c == null)
            {
                c = DbContext.Cart.Add(cart);
            }

            foreach (var cd in cart.CartDetails)
            {
                cd.CartID = c.ID;
                DbContext.CartDetail.Add(cd);
            }
            return c;
        }
        public override void Update(Cart cart)
        {
            var CartDetails = DbContext.CartDetail.Where(x => x.CartID == x.ID).Select(x => x.ID);
            foreach (var c in cart.CartDetails)
            {
                if (!CartDetails.Contains(c.ID))
                {
                    DbContext.CartDetail.Add(c);
                }
            }
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

        public void ChangeTable(Cart cart)
        {
            //check cart exist in table

            var result = DbContext.Cart.FirstOrDefault(x => x.TableID == cart.TableID);
            if (result != null)
            {
                var listCD = DbContext.CartDetail.Where(x => x.CartID == result.ID);
                foreach (var cd in cart.CartDetails)
                {
                    if (listCD.Any(x => x.ProID == cd.ProID & x.Note.Trim() == cd.Note.Trim()))
                    {
                        var newCd = DbContext.CartDetail.FirstOrDefault(x => x.CartID == result.ID & x.ProID == cd.ProID & x.Note == cd.Note);
                        newCd.Quantity += cd.Quantity;
                    }
                    else
                    {
                        cd.CartID = result.ID;
                        DbContext.CartDetail.Add(cd);
                    }
                }
            }
            else
            {
                Add(cart);
            }
            var oldCart = DbContext.Cart.FirstOrDefault(x => x.ID == cart.ID);
            DbContext.Cart.Remove(oldCart);
        }
    }
}