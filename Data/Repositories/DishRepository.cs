using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        IEnumerable<Dish> GetDishByCombo(int comboId);

        IEnumerable<Dish> GetDishByCategory(int categoryId);

        IEnumerable<DishViewModel> GetAll();

        IEnumerable<Dish> GetTopHot();

        void  IsSold(List<UpdateSoldViewModel> list);

        int GetDishCount();
    }

    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        public DishRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<DishViewModel> GetAll()
        {
            var query = from d in DbContext.Dishes
                        join ct in DbContext.DishCategories
                        on d.CategoryID equals ct.ID
                        select new DishViewModel()
                        {
                            ID = d.ID,
                            Image = d.Image,
                            Name = d.Name,
                            OrderCount = d.OrderCount,
                            Price = d.Price,
                            Status = d.Status,
                            Amount = d.Amount,
                            CategoryID = d.CategoryID,
                            CreatedDate = d.CreatedDate,
                            Description = d.Description,
                            CategoryName = ct.Name
                        };
            return query;
        }

        public IEnumerable<Dish> GetDishByCategory(int categoryId)
        {
            var query = from d in DbContext.Dishes
                        where d.CategoryID == categoryId
                        select d;
            return query;
        }

        public IEnumerable<Dish> GetDishByCombo(int comboId)
        {
            var query = from d in DbContext.Dishes
                        join dc in DbContext.DishComboMapping on d.ID equals dc.DishID
                        join c in DbContext.Combos on dc.ComboID equals c.ID
                        where c.ID == comboId
                        select d;
            return query;
        }

        public int GetDishCount()
        {
            return DbContext.Dishes.Count();
        }

        public IEnumerable<Dish> GetTopHot()
        {
            return DbContext.Database.SqlQuery<Dish>("GetTop10Dish");
        }

        public void IsSold(List<UpdateSoldViewModel> list)
        {
            foreach(UpdateSoldViewModel pro in list)
            {
                switch (pro.Type)
                {
                    case 1:
                        var dish = DbContext.Dishes.SingleOrDefault(x => x.ID == pro.Id);
                        if (dish != null && dish.Amount >= pro.Amount)
                        {
                            dish.Amount = dish.Amount- pro.Amount;
                            dish.OrderCount = dish.OrderCount + pro.Amount;
                        }
                    break;
                    case 2:
                        var combo = DbContext.Combos.SingleOrDefault(x => x.ID == pro.Id);
                        if (combo != null && combo.Amount >= pro.Amount)
                        {
                            combo.Amount = combo.Amount - pro.Amount;
                        }
                        break;

                }
            }
            
        }
    }
}