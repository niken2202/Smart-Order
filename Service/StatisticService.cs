using Common.ViewModels;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate, DateTime toDate);
    }
    public class StatisticService : IStatisticService
    {
        IBillRepository billRepository;

        public StatisticService(IBillRepository billRepository)
        {
            this.billRepository = billRepository;
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate, DateTime toDate)
        {
            return billRepository.GetRevenueStatistic(fromdate, toDate);
        }
    }
}
