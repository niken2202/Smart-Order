using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IHistoryService
    {
        History Add(History history);

        IEnumerable<History> GetAll();

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

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}