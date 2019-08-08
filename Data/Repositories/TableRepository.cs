using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Data.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        
        Table SaveIMEI(int tableID, string imei);
        IEnumerable<Table> GetVariableTable();
    }

    public class TableRepository : RepositoryBase<Table>, ITableRepository
    {
        public TableRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public override Table Delete(int id)
        {
            Table table = DbContext.Tables.SingleOrDefault(m => m.ID == id); ;
            DbContext.Tables.Remove(table);
            return table;
        }

        public IEnumerable<Table> GetVariableTable()
        {
            var query = from t in DbContext.Tables
                        where t.DeviceID.Trim().Length == 0
                        select t;
            return query;
        }

        public Table SaveIMEI(int tableID, string imei)
        {
            Table table = DbContext.Tables.SingleOrDefault(m => m.ID == tableID);
            
            if (table != null)
            {
                table.DeviceID = imei;
                Update(table);
            }
            return table;
        }
    }
}