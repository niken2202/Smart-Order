using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IApplicationUserRoleRepository : IRepository<ApplicationUserRole>
    {
    }

    public class ApplicationUserRoleRepository : RepositoryBase<ApplicationUserRole>, IApplicationUserRoleRepository
    {
        public ApplicationUserRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}