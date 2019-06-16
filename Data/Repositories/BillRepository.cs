using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IBillRepository: IRepository<Bill>{

    }
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }

}