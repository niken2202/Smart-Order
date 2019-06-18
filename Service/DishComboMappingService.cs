using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IDishComboMappingService
    {
        DishComboMapping Add(DishComboMapping dishComboMapping);
        DishComboMapping Delete(int id);
        void Update(DishComboMapping dishComboMapping);
        IEnumerable<DishComboMapping> GetAll();
        void SaveChanges();
    }

    public class DishComboMappingService : IDishComboMappingService
    {
        private IUnitOfWork unitOfWork;
        private IDishComboMappingRepository dishComboMappingRepository;

        public DishComboMappingService(IUnitOfWork unitOfWork, IDishComboMappingRepository dishComboMappingRepository)
        {
            this.unitOfWork = unitOfWork;
            this.dishComboMappingRepository = dishComboMappingRepository;
        }

        public DishComboMapping Add(DishComboMapping dishComboMapping)
        {
            return dishComboMappingRepository.Add(dishComboMapping);
        }

        public DishComboMapping Delete(int id)
        {
            return dishComboMappingRepository.Delete(id);
        }

        public IEnumerable<DishComboMapping> GetAll()
        {
            return dishComboMappingRepository.GetAll();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(DishComboMapping dishComboMapping)
        {
             dishComboMappingRepository.Update(dishComboMapping);
        }
    }
}