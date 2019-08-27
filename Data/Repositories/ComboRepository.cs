using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        ComboViewModel GetComboById(int id);
        Combo DisableCombo(int id);
    }

    public class ComboRepository : RepositoryBase<Combo>, IComboRepository
    {
        public ComboRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public override Combo Add(Combo entity)
        {
            var c = DbContext.Combos.Add(entity);
            if (c != null)
            {
                foreach(var dc in entity.DishComboMappings)
            {
                dc.ComboID = c.ID;
                DbContext.DishComboMapping.Add(dc);
            }
            }
           
            return c;
        }

        public Combo DisableCombo(int id)
        {
            var combo = DbContext.Combos.SingleOrDefault(x => x.ID == id);
            if (combo != null)
            {
                combo.Status = false;
            }
            return combo;
        }

        public ComboViewModel GetComboById(int id)
        {
            var combo = (from c in DbContext.Combos
                        where c.ID == id
                        select c).FirstOrDefault();

            var dishes = from d in DbContext.Dishes
                         join dc in DbContext.DishComboMapping on d.ID equals dc.DishID
                         where dc.ComboID == id
                         select new DishComboViewModel()
                         {
                            ID= d.ID,
                             Name= d.Name,
                             Price=d.Price,
                             Status= d.Status,
                             Description= d.Description,
                             CategoryID= d.CategoryID,
                             CreatedDate= d.CreatedDate,
                             Amount=dc.Amount,
                             Image=  d.Image,
                         };
            if (combo == null) return null;
          var cc = new ComboViewModel
            {
              ID= combo.ID,
              Image=  combo.Image,
              Name=  combo.Name,
              Price=  combo.Price,
              Amount= combo.Amount,
              CreatedDate = combo.CreatedDate,
              Description= combo.Description,
              Status=  combo.Status,
              dishes=  dishes
            };
            return cc;

        }
        public override void Update(Combo entity)
        {
            var combo = DbContext.Combos.SingleOrDefault(x => x.ID == entity.ID);
            if (combo != null)
            {
                foreach (var dc in entity.DishComboMappings)
                {
                    var old = DbContext.DishComboMapping.SingleOrDefault(x => x.DishID == dc.DishID && x.ComboID == entity.ID);
                    if (old != null)
                    {
                        old.Amount = old.Amount + (dc.Amount - old.Amount);
                    }
                    else
                    {
                        dc.ComboID = entity.ID;
                        DbContext.DishComboMapping.Add(dc);
                    }
                }
            }
        }
    }
}