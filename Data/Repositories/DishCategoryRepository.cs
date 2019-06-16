using Data.Infrastructure;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
