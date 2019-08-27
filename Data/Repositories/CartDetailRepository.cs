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
        public override CartDetail Delete(int id)
        {
            var cd = DbContext.CartDetail.SingleOrDefault(x => x.ID == id);
            if (cd != null)
            {
                switch (cd.Type)
                {
                    case 1:
                        var d = DbContext.Dishes.SingleOrDefault(x => x.ID == cd.ProID);
                        if (cd.Quantity >= 0)
                        {
                            d.Amount = d.Amount+ cd.Quantity;
                            if (d.Amount > 0) d.Status = 1;
                        }
                        break;
                    case 2:
                        var combo = DbContext.Combos.SingleOrDefault(x => x.ID == cd.ProID);
                        if (cd.Quantity >= 0)
                        {
                            combo.Amount = combo.Amount + cd.Quantity;
                            if (combo.Amount > 0) combo.Status = true;
                        }
                        break;
                }
                DbContext.CartDetail.Remove(cd);
            }
          
            return cd;

        }
        public override CartDetail Add(CartDetail cartDetail)
        {
            var old = DbContext.CartDetail.SingleOrDefault(x => x.CartID == cartDetail.CartID && x.ID == cartDetail.ID && x.ProID == cartDetail.ProID);
            int change = 0;
            //Add to cartdetail
            if (old != null && (old.Note == null && cartDetail.Note == null || (cartDetail.Note.Trim().Equals(old.Note.Trim()))))
            {
                change = cartDetail.Quantity - old.Quantity;
                old.Quantity = old.Quantity + change;
                if (old.Quantity < 0) old.Quantity = 0;
            }
             else {
                old = DbContext.CartDetail.Add(cartDetail);
                change = old.Quantity;
            }
            // subtract Dish and combo
            switch (cartDetail.Type)
            {
                case 1:
                    var d = DbContext.Dishes.SingleOrDefault(x => x.ID == old.ProID);
                    d.Amount = d.Amount - change;
                    if (d.Amount <= 0)
                    {
                        d.Amount = 0;
                        d.Status = 0;
                    }
                    else
                    {
                        d.Status = 1;
                    }
                    break;
                case 2:
                    var combo = DbContext.Combos.SingleOrDefault(x => x.ID == old.ProID);
                    combo.Amount = combo.Amount -change;
                    if (combo.Amount <= 0)
                    {
                        combo.Amount = 0;
                        combo.Status = false;
                    }
                    else
                    {
                        combo.Status = true;
                    }
                    break;
            }

            return old;
        }
    }
}