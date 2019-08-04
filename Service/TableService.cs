using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface ITableService
    {
        Table Add(Table table);

        IEnumerable<Table> GetAll();

        Table GetById(int id);

        Table Delete(int id);
        void Update(Table table);

        IEnumerable<Table> GetByName(string name);

        void SaveChanges();
    }

    public class TableService : ITableService
    {
        private IUnitOfWork unitOfWork;
        private ITableRepository tableRepository;

        public TableService(IUnitOfWork unitOfWork, ITableRepository tableRepository)
        {
            this.unitOfWork = unitOfWork;
            this.tableRepository = tableRepository;
        }

        public Table Add(Table table)
        {
            return tableRepository.Add(table);
        }

        public Table Delete(int id)
        {
            return tableRepository.Delete(id);
        }


        public IEnumerable<Table> GetAll()
        {
            return tableRepository.GetAll();
        }

        public Table GetById(int id)
        {
            return tableRepository.GetSingleById(id);
        }

        public IEnumerable<Table> GetByName(string name)
        {
            return null;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(Table table)
        {
            tableRepository.Update(table);
        }
    }
}