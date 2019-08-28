using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<string> GetRoleByUserID(string id);
        IEnumerable<ApplicationRole> GetAllRoles();
    }

    public class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<ApplicationRole> GetAllRoles()
        {
            return DbContext.Database.SqlQuery<ApplicationRole>("GetAllRole");
        }
        public IEnumerable<string> GetRoleByUserID(string id)
        {
            var parameters = new object[]
           {
                new SqlParameter("@userid",id),
           };
            return DbContext.Database.SqlQuery<string>("GetRoleByUserID @userid", parameters);
        }
    }
}