using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IDishService
    {
        Dish Add(Dish dish);
        IEnumerable<DishViewModel> GetAll();
        Dish GetById(int id);
      //  IEnumerable<Bill> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        Dish GetByName(string name);
        void Update(Dish dish);
        Dish Delete(int id);
        IEnumerable<Dish> GetAllByComboId(int comboId);
        IEnumerable<Dish> GetByCategogy(int cateId);
        IEnumerable<Dish> GetTopDish();
        int GetDishCount();
        void SaveChanges();
        Dish DisableDish(int id);
        void IsSold(List<UpdateSoldViewModel> list);
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

        public Dish DisableDish(int id)
        {
            return dishRepository.DisableDish(id);
        }

        public IEnumerable<DishViewModel> GetAll()
        {
            return dishRepository.GetAll();
        }

        public IEnumerable<Dish> GetAllByComboId(int comboId)
        {
            return dishRepository.GetDishByCombo(comboId);
        }

        public IEnumerable<Dish> GetByCategogy(int cateId)
        {
            return dishRepository.GetDishByCategory(cateId);
        }

        public Dish GetById(int id)
        {
            return dishRepository.GetSingleById(id);
        }

        public Dish GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public int GetDishCount()
        {
            return dishRepository.GetDishCount();
        }

        public IEnumerable<Dish> GetTopDish()
        {
            return dishRepository.GetTopHot();
        }

        public void IsSold(List<UpdateSoldViewModel> list)
        {
            dishRepository.IsSold(list);
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