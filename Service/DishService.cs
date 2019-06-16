using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IDishService
    {
        Dish Add(Dish dish);
        IEnumerable<Dish> GetAll();
        Dish GetById(int id);
        Dish GetByName(string name);
        void Update(Dish dish);
        Dish Delete(int id);
        IEnumerable<Dish> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        void SaveChanges();
    }

    public class DishService : IDishService
    {
        private IUnitOfWork unitOfWork;
        private IDishRepository dishRepository;

        public DishService(IUnitOfWork unitOfWork, IDishRepository dishRepository)
        {
            this.unitOfWork = unitOfWork;
            this.dishRepository = dishRepository;
        }
        public Dish Add(Dish dish)
        {
            return dishRepository.Add(dish);
        }

        public Dish Delete(int id)
        {
            return dishRepository.Delete(id);
        }

        public IEnumerable<Dish> GetAll()
        {
            return dishRepository.GetAll();
        }

        public IEnumerable<Dish> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            throw new System.NotImplementedException();
        }

        public Dish GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Dish GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(Dish dish)
        {
            dishRepository.Update(dish);
        }
    }
}