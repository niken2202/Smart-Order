using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IBillDetailRepository : IRepository<BillDetail>
    {
        IEnumerable<BillDetail> GetByBillID(int id);
    }

    public class BillDetailRepository : RepositoryBase<BillDetail>, IBillDetailRepository
    {
        public BillDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<BillDetail> GetByBillID(int id)
        {
            var listDishOfBill = from dish in DbContext.BillDetail
                                 where dish.BillID == id
                                select dish  ;
            return listDishOfBill;
        }
    }
}