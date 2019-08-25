using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
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
            var listTable = DbContext.Tables.Where(x => x.DeviceID == null || x.DeviceID.Trim().Length == 0);
            return listTable;
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