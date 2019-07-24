using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        IEnumerable<Combo> GetAll();
        object GetComboById(int id);

    }

    public class ComboRepository : RepositoryBase<Combo>, IComboRepository
    {
        public ComboRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Combo> GetAll()
        {
            var listc = from c in DbContext.Combos
                        select c;
            return listc;
        }

        
        public object GetComboById(int id)
        {
            var combo = (from c in DbContext.Combos
                        where c.ID == id
                        select c).FirstOrDefault();

            var dishes = from d in DbContext.Dishes
                         join dc in DbContext.DishComboMapping on d.ID equals dc.ComboID
                         where dc.ComboID == id
                         select new
                         {
                             d.ID,
                             d.Name,
                             d.Price,
                             d.Status,
                             d.Description,
                             d.CategoryID,
                             d.CreatedDate,
                             dc.Amount,
                             d.Image,
                         };
            if (combo == null) return null;
          var cc = new
            {
                combo.ID,
                combo.Image,
                combo.Name,
                combo.Price,
                combo.Amount,
                combo.Description,
                combo.Status,
                dishes
            };
            return cc;

        }
    }
}