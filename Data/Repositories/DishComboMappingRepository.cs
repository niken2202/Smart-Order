using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishComboMappingRepository : IRepository<DishComboMapping>
    {
    }

    public class DishComboMappingRepository : RepositoryBase<DishBillMapping>, IDishBillMappingRepository
    {
        public DishComboMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}