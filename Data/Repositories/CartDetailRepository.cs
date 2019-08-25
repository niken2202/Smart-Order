using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
        IEnumerable<CartDetailViewModel> GetAll();

    }

    public class CartDetailRepository : RepositoryBase<CartDetail>, ICartDetailRepository
    {
       
        public CartDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public IEnumerable<CartDetailViewModel> GetAll()
        {
            var cartDetail = DbContext.Database.SqlQuery<CartDetailViewModel>("GetAllCartDetail");
            return cartDetail;
        }
        public override CartDetail Add(CartDetail cartDetail)
        {
            var cd = DbContext.CartDetail.SingleOrDefault(x => x.CartID == cartDetail.CartID && x.ID == cartDetail.ID && x.ProID == cartDetail.ProID);
            if (cd != null && (cd.Note == null && cartDetail.Note == null || (cartDetail.Note.Trim().Equals(cd.Note.Trim()))))
            {
                if (cd.Quantity < cartDetail.Quantity)
                {
                    cd.Quantity = cartDetail.Quantity;
                }
            }
             else {
               return DbContext.CartDetail.Add(cartDetail);
            }
            return cd;
        }
    }
}