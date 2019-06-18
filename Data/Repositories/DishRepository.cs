using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Data.Repositories
{
    public interface IDishRepository : IRepository<Dish>
    {
        IEnumerable<Dish> GetDishByCombo(int comboId, int pageIndex, int pageSize, out int totalRow);
    }

    public class DishRepository : RepositoryBase<Dish>, IDishRepository
    {
        public DishRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Dish> GetDishByCombo(int comboId, int pageIndex, int pageSize, out int totalRow)
        {
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