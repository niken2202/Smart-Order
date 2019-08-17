using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IMaterialRepository : IRepository<Material>
    {
        IEnumerable<Material> GetAllByDishID(int dishId);
        IEnumerable<Material> GetAll();
    }

    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<Material> GetAll()
        { 
            var query = from d in DbContext.Materials
                        select d;
           
            return query;
        }
        
        public IEnumerable<Material> GetAllByDishID(int dishId)
        {
            var query = from m in DbContext.Materials
                        join dm in DbContext.DishMaterialMappings on m.ID equals dm.MaterialID
                        join d in DbContext.Dishes on dm.DishID equals d.ID
                        where d.ID == dishId
                        select m;
            return query;
        }
    }
}