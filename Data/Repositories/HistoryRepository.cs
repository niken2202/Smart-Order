using Data.Infrastructure;
using Model.Models;


namespace Data.Repositories
{
    public interface IHistoryRepository : IRepository<History> { }

    public class HistoryRepository : RepositoryBase<History>, IHistoryRepository
    {
        public HistoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
