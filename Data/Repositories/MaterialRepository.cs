using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        IEnumerable<Material> GetAllByDishID(int dishId, int pageIndex, int pageSize, out int totalRow);

    }

    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Material> GetAllByDishID(int dishId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from m in DbContext.Materials
                        join dm in DbContext.DishMaterialMappings on m.ID equals dm.MaterialID
                        join d in DbContext.Dishes on dm.DishID equals d.ID
                        where d.ID == dishId
                        select m;

            totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;

        }
    }
}