﻿using Common.ViewModels;
using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        IEnumerable<Combo> GetAll();
        ComboViewModel GetComboById(int id);

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

        
        public ComboViewModel GetComboById(int id)
        {
            var combo = (from c in DbContext.Combos
                        where c.ID == id
                        select c).FirstOrDefault();

            var dishes = from d in DbContext.Dishes
                         join dc in DbContext.DishComboMapping on d.ID equals dc.ComboID
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