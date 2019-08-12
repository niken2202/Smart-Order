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
        
    }
}