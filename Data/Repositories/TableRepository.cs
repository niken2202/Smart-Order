using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        Table Delete(int id);
    }

    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Table Delete(int id)
        {
            Table table = DbContext.Tables.SingleOrDefault(m => m.ID == id); ;
            DbContext.Tables.Remove(table);
            return table;
        }
    }
}