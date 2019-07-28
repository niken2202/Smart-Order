using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}