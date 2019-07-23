using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
        Combo GetById(int id);

        IEnumerable<object> GetAll();
    }

    public class ComboRepository : RepositoryBase<Combo>, IComboRepository
    {
        public ComboRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<object> GetAll()
        {
            var listc = from c in DbContext.Combos
                        select c;

            List<object> listCombo = new List<object>();
            foreach (var combo in listc)
            {
                var dishes = from d in DbContext.Dishes
                             join dc in DbContext.DishComboMapping on d.ID equals dc.ComboID
                             where dc.ComboID == combo.ID
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

                listCombo.Add(new
                {
                    combo.ID,
                    combo.Image,
                    combo.Name,
                    combo.Price,
                    combo.Amount,
                    combo.Description,
                    combo.Status,
                    dishes
                });
            }
            return listCombo;
        }

        public Combo GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}