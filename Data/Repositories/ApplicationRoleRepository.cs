using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<string> GetRoleByUserID(string id);
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<string> GetRoleByUserID(string id)
        {
            var Roles = (from ur in DbContext.ApplicationUserRoles
                        join r in DbContext.ApplicationRoles on ur.RoleId equals r.Id
                        where ur.UserId == id
                        select r.Name).ToList();
            return Roles;
        }
    }
}