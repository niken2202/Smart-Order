using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        IEnumerable<Dish> GetDishByCombo(int comboId, int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Dish> GetAll(int page, int pageSize, out int totalRow);

    }

    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        public DishRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Dish> GetAll(int pageIndex, int pageSize, out int totalRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            var query = from d in DbContext.Dishes
                      select d ;
            totalRow = query.Count();

            query = query.OrderBy(i => i.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }

        public IEnumerable<Dish> GetDishByCombo(int comboId, int pageIndex, int pageSize, out int totalRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            var query = from d in DbContext.Dishes
                        join dc in DbContext.DishComboMapping on d.ID equals dc.DishID
                        join c in DbContext.Combos on dc.ComboID equals c.ID
                        where c.ID == comboId
                        orderby c.ID
                        select d;

            totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}