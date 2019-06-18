using Data.Infrastructure;
using Model.Models;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IComboRepository : IRepository<Combo>
    {
       
    }

    public class ComboRepository : RepositoryBase<Combo>, IComboRepository
    {
        public ComboRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}