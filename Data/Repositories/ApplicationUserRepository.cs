using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {

    }
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}