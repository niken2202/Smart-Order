using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishBillMappingRepository : IRepository<DishBillMapping>
    {
    }

    public class DishBillMappingRepository : RepositoryBase<DishBillMapping>, IDishBillMappingRepository
    {
        public DishBillMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}