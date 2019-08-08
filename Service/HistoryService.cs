using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IHistoryService
    {
        History Add(History history);
        IEnumerable<History> GetAll();
        IEnumerable<History> GetTimeRange(DateTime fromDate, DateTime toDate);
        IEnumerable<History> GetHistoryLastMonth();
        IEnumerable<History> GetHistoryLast7Days();
        IEnumerable<History> GetHistoryToday();

        void SaveChanges();
    }

    public class HistoryService : IHistoryService
    {
        private IUnitOfWork unitOfWork;
        private IHistoryRepository historyRepository;

        public HistoryService(IUnitOfWork unitOfWork, IHistoryRepository historyRepository)
        {
            this.unitOfWork = unitOfWork;
            this.historyRepository = historyRepository;
        }

        public History Add(History history)
        {
            return historyRepository.Add(history);
        }

        public IEnumerable<History> GetAll()
        {
            return historyRepository.GetAll();
        }

        public IEnumerable<History> GetHistoryLast7Days()
        {
            return historyRepository.GetHistoryLast7Days();
        }

        public IEnumerable<History> GetHistoryLastMonth()
        {
            return historyRepository.GetHistoryLastMonth();
        }

        public IEnumerable<History> GetHistoryToday()
        {
            return historyRepository.GetHistoryToday();
        }

        public IEnumerable<History> GetTimeRange(DateTime fromDate, DateTime toDate)
        {
            return historyRepository.GetTimeRange( fromDate, toDate);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}