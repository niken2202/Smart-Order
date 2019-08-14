using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Data.Infrastructure;
using Model.Models;

namespace Data.Repositories
{
    public interface IDishComboMappingRepository : IRepository<DishComboMapping>
    {
    }

    public class DishComboMappingRepository : RepositoryBase<IDishComboMappingRepository>, IDishComboMappingRepository
    {
        public DishComboMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public DishComboMapping Add(DishComboMapping entity)
        {
            throw new NotImplementedException();
        }

        public bool CheckContains(Expression<Func<DishComboMapping, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<DishComboMapping, bool>> where)
        {
            throw new NotImplementedException();
        }

        public DishComboMapping Delete(DishComboMapping entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteMulti(Expression<Func<DishComboMapping, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishComboMapping> GetMulti(Expression<Func<DishComboMapping, bool>> predicate, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DishComboMapping> GetMultiPaging(Expression<Func<DishComboMapping, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public DishComboMapping GetSingleByCondition(Expression<Func<DishComboMapping, bool>> expression, string[] includes = null)
        {
            throw new NotImplementedException();
        }

        public void Update(DishComboMapping entity)
        {
            throw new NotImplementedException();
        }

        DishComboMapping IRepository<DishComboMapping>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DishComboMapping> IRepository<DishComboMapping>.GetAll(string[] includes)
        {
            throw new NotImplementedException();
        }

        DishComboMapping IRepository<DishComboMapping>.GetSingleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}