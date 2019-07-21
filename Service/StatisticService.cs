using Common.ViewModels;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromdate, DateTime toDate);
    }

    public class StatisticService : IStatisticService
    {
        private IBillRepository billRepository;

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