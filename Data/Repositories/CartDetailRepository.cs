using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface ICartDetailRepository : IRepository<CartDetail>
    {
    }

    public class CartDetailRepository : RepositoryBase<CartDetail>, ICartDetailRepository
    {
        public CartDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}