using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IComboService
    {
        Combo Add(Combo combo);

        IEnumerable<Combo> GetAll();

        Combo GetById(int id);

        Combo Delete(int id);

        void Update(Combo combo);

        object GetCombobyId(int id);
        void SaveChanges();
    }

    public class ComboService : IComboService
    {
        private IUnitOfWork unitOfWork;
        private IComboRepository comboRepository;

        public ComboService(IUnitOfWork unitOfWork, IComboRepository comboRepository)
        {
            this.unitOfWork = unitOfWork;
            this.comboRepository = comboRepository;
        }

        public Combo Add(Combo combo)
        {
           return comboRepository.Add(combo);
        }

        public Combo Delete(int id)
        {
            return comboRepository.Delete(id);
        }

        public IEnumerable<object> GetAll()
        {
            return comboRepository.GetAll();
        }

        public Combo GetById(int id)
        {
            return comboRepository.GetSingleById(id);
        }

        public object GetCombobyId(int id)
        {
            return comboRepository.GetComboById(id);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(Combo combo)
        {
            comboRepository.Update(combo);
        }

        IEnumerable<Combo> IComboService.GetAll()
        {
            return comboRepository.GetAll();
        }
    }
}