using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace Data.Repositories
{
    public interface IBillRepository : IRepository<Bill>
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromDate, DateTime toDate);
        IEnumerable<RevenueByMonthViewModel> GetRevenueGroupByMonth(DateTime fromdate, DateTime toDate);
        IEnumerable<Bill> GetTimeRange(DateTime fromDate, DateTime toDate);
        IEnumerable<Bill> GetBillLastMonth();
        IEnumerable<Bill> GetBillLast7Days();
        IEnumerable<Bill> GetBillToday();
        IEnumerable<BillViewModel> GetAll();
        object GetBillDetail(int id);
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
        public object GetBillDetail(int id)
        {
            Bill bill = GetSingleById(id);
            if (bill == null) return null;
            var query = from bd in DbContext.BillDetail
                        where bd.BillID == id
                        select bd;

            bill.BillDetail = query.ToList();
            var total = query.Sum(i => (i.Amount * i.Price));

            var b = new
            {
                bill.ID,
                bill.TableID,
                bill.Status,
                bill.Voucher,
                bill.CustomerName,
                bill.CreatedDate,
                bill.CreatedBy,
                bill.Content,
                bill.BillDetail,
                bill.Discount,
                Total = total
            };
            return b;
        }
        public override Bill Add(Bill bill)
        {
            Bill b = DbContext.Bills.Add(bill);
            foreach (var bd in bill.BillDetail)
            {
                bd.BillID = b.ID;
                DbContext.BillDetail.Add(bd);
            }
            var cart = DbContext.Cart.SingleOrDefault(m => m.ID == bill.TableID);
            if (cart != null)
            {
                DbContext.Cart.Remove(cart);
            }
            var code = DbContext.PromotionCode.SingleOrDefault(x => x.Code.Equals(b.Voucher));
            if (code.Times > 0)
            {
                code.Times--;
            }
            if (code.Times <= 0) code.Status = false;
            return b;
        }
        public IEnumerable<BillViewModel> GetAll()
        {
            var query = from d in DbContext.Bills
                        join t in DbContext.Tables on d.TableID equals t.ID
                        select new BillViewModel()
                        {
                            ID= d.ID,
                            TableID= d.TableID,
                            CustomerName=d.CustomerName,
                            Voucher= d.Voucher,
                            Discount=d.Discount,
                            Content=d.Content,
                            CreatedBy=d.CreatedBy,
                            CreatedDate=d.CreatedDate,
                            Status=d.Status,
                            TableName= t.Name
                        };
            return query;
        }

        public IEnumerable<RevenueByMonthViewModel> GetRevenueGroupByMonth(DateTime fromDate, DateTime toDate)
        {
            var parameters = new object[]
               {
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate),
               };
            var revenue = DbContext.Database.SqlQuery<RevenueByMonthViewModel>("GetRevenueByMonth @fromDate,@toDate", parameters);
            return revenue;
        }
    }

}