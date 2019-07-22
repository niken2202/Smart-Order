using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishCategoryRepository : IRepository<DishCategory>
    {
    }

    public class DishCategoryRepository : RepositoryBase<DishCategory>, IDishCategoryRepository
    {
        public DishCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}