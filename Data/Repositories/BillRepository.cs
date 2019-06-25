using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IBillRepository: IRepository<Bill>{

        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate,DateTime toDate);
    }
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(IDbFactory dbFactory) : base(dbFactory)
        {

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