using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IDishService
    {
        Dish Add(Dish dish);
        IEnumerable<Dish> GetAll(int page, int pageSize, out int totalRow);
        Dish GetById(int id);
      //  IEnumerable<Bill> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        Dish GetByName(string name);
        void Update(Dish dish);
        Dish Delete(int id);
        IEnumerable<Dish> GetAllByComboId(int comboId, int page, int pageSize, out int totalRow);
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

        public IEnumerable<Dish> GetAll(int pageIndex, int pageSize, out int totalRow)
        {
            return dishRepository.GetAll(pageIndex,pageSize,out totalRow);
        }

        public IEnumerable<Dish> GetAllByComboId(int comboId, int page, int pageSize, out int totalRow)
        {
            return dishRepository.GetDishByCombo(comboId, page, pageSize,out totalRow);
        }

  

        public Dish GetById(int id)
        {
          return dishRepository.GetSingleById(id);
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