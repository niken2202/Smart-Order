using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IBillDetailRepository : IRepository<BillDetail>
    {
    }

    public class BillDetailRepository : RepositoryBase<BillDetail>, IBillDetailRepository
    {
        public BillDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}