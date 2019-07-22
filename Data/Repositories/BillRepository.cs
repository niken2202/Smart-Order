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
        IEnumerable<Bill> GetAll();
        IEnumerable<Bill> GetTimeRange(DateTime fromDate,DateTime toDate);
        IEnumerable<Bill> GetBillLastMonth();
        IEnumerable<Bill> GetBillLast7Days();
        IEnumerable<Bill> GetBillToday();
    }
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Bill> GetAll()
        {
            
            var query = from d in DbContext.Bills
                        select d;
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

        public IEnumerable<Bill> GetTimeRange(DateTime fromDate, DateTime toDate)
        {
            var parameters = new object[]
               {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
               };
            return DbContext.Database.SqlQuery<Bill>("GetBillByRange @fromDate,@toDate", parameters);
        }
        public IEnumerable<Bill> Get(DateTime fromDate, DateTime toDate)
        {
            var parameters = new object[]
               {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
               };
            return DbContext.Database.SqlQuery<Bill>("GetBillByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<Bill> GetBillLastMonth()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-30);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<Bill>("GetBillByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<Bill> GetBillLast7Days()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-7);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<Bill>("GetBillByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<Bill> GetBillToday()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-1);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<Bill>("GetBillByRange @fromDate,@toDate", parameters);
        }
    }

}