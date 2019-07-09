using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Data.Repositories
{
    public interface IBillRepository: IRepository<Bill>{

        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate,DateTime toDate);
        IEnumerable<Bill> GetAll(int pageIndex, int pageSize, out int totalRow);
    }
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Bill> GetAll(int pageIndex, int pageSize, out int totalRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            var query = from d in DbContext.Bills
                        select d;
            totalRow = query.Count();

            query = query.OrderBy(i => i.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate, DateTime toDate)
        {
            var parameters = new object[]
            {
                new SqlParameter("@fromDate",fromdate),
                new SqlParameter("@toDate",toDate),
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        }

     
    }

}