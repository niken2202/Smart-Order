using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishRepository : IRepository<DishCategory>
    {
    }

    public class DishRepository : RepositoryBase<DishCategory>, IDishCategoryRepository
    {
        public DishRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}