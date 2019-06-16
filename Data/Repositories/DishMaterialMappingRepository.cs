using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishMaterialMappingRepository : IRepository<DishMaterialMapping>
    {
    }

    public class DishMaterialMappingRepository : RepositoryBase<DishMaterialMapping>, IDishMaterialMappingRepository
    {
        public DishMaterialMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}