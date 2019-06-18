using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IDishCategoryService
    {
        DishCategory Add(DishCategory dishCategory);

        IEnumerable<DishCategory> GetAll();

        DishCategory GetById(int id);

        DishCategory Delete(int id);

        void Update(DishCategory dishCategory);

        IEnumerable<DishCategory> GetByName(string name);

        void SaveChanges();
    }

    public class DishCategoryService : IDishCategoryService
    {
        private IDishCategoryRepository dishCategoryRepository;
        private IUnitOfWork unitOfWork;

        public DishCategoryService(IDishCategoryRepository dishCategoryRepository, IUnitOfWork unitOfWork)
        {
            this.dishCategoryRepository = dishCategoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public DishCategory Add(DishCategory dishCategory)
        {
            return dishCategoryRepository.Add(dishCategory);
        }

        public DishCategory Delete(int id)
        {
            return dishCategoryRepository.Delete(id);
        }

        public IEnumerable<DishCategory> GetAll()
        {
            return dishCategoryRepository.GetAll();
        }

        public DishCategory GetById(int id)
        {
            return dishCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<DishCategory> GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(DishCategory material)
        {
            dishCategoryRepository.Update(material);
        }
    }
}