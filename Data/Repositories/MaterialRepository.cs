using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IMaterialRepository : IRepository<Material> { }

    public class MaterialRepository : RepositoryBase<Material>, IMaterialRepository
    {
        public MaterialRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}