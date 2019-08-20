﻿using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        ComboViewModel GetComboById(int id);
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
              Description= combo.Description,
              Status=  combo.Status,
              dishes=  dishes
            };
            return cc;

        }
    }
}