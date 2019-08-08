using Data.Infrastructure;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.Repositories
{
    public interface IHistoryRepository : IRepository<History> {
        IEnumerable<History> GetTimeRange(DateTime fromDate, DateTime toDate);
        IEnumerable<History> GetHistoryLastMonth();
        IEnumerable<History> GetHistoryLast7Days();
        IEnumerable<History> GetHistoryToday();
    }

    public class HistoryRepository : RepositoryBase<History>, IHistoryRepository
    {
        public HistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<History> GetHistoryLast7Days()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-7);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<History>("GetHistoryByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<History> GetHistoryLastMonth()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-30);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<History>("GetHistoryByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<History> GetHistoryToday()
        {
            DateTime toDate = DateTime.Now;
            DateTime fromDate = toDate.AddDays(-1);

            var parameters = new object[]
              {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
              };
            return DbContext.Database.SqlQuery<History>("GetHistoryByRange @fromDate,@toDate", parameters);
        }

        public IEnumerable<History> GetTimeRange(DateTime fromDate, DateTime toDate)
        {
            var parameters = new object[]
                {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
                };
            return DbContext.Database.SqlQuery<History>("GetHistoryByRange @fromDate,@toDate", parameters);
        }
    }
}
