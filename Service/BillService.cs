using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IBillService
    {
        Bill Add(Bill bill);

        Bill GetById(int id);

        IEnumerable<Bill> GetAll();

        IEnumerable<Bill> GetTimeRange(DateTime fromDate, DateTime toDate);

        IEnumerable<Bill> GetBillLastMonth();

        IEnumerable<Bill> GetBillLast7Days();

        IEnumerable<Bill> GetBillToday();
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromDate, DateTime toDate);

        object GetBillDetail(int id);

        void SaveChanges();
    }

    public class BillService : IBillService
    {
        private IUnitOfWork unitOfWork;
        private IBillRepository billRepository;

        public BillService(IUnitOfWork unitOfWork, IBillRepository billRepository)
        {
            this.unitOfWork = unitOfWork;
            this.billRepository = billRepository;
        }

        public Bill Add(Bill bill)
        {
            return billRepository.Add(bill);
        }

        public IEnumerable<Bill> GetAll()
        {
            return billRepository.GetAll();
        }

        public object GetBillDetail(int id)
        {
            return billRepository.GetBillDetail(id);
        }

        public IEnumerable<Bill> GetBillLast7Days()
        {
            return billRepository.GetBillLast7Days();
        }

        public IEnumerable<Bill> GetBillLastMonth()
        {
            return billRepository.GetBillLastMonth();
        }

        public IEnumerable<Bill> GetBillToday()
        {
            return billRepository.GetBillToday();
        }

        public Bill GetById(int id)
        {
            return billRepository.GetSingleById(id);
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(DateTime fromDate, DateTime toDate)
        {
            return billRepository.GetRevenueStatistic(fromDate, toDate);
        }

        public IEnumerable<Bill> GetTimeRange(DateTime fromDate, DateTime toDate)
        {
            return billRepository.GetTimeRange(fromDate, toDate);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}