using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
    }

    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        public DishRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}