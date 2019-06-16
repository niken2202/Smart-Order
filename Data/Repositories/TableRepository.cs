using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
    }

    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}