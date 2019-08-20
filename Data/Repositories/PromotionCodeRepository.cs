using Data.Infrastructure;
using Model.Models;
using System.Linq;
namespace Data.Repositories
{
    public interface IPromotionCodeRepository : IRepository<PromotionCode>
    {
      
    }

    public class PromotionCodeRepository : RepositoryBase<PromotionCode>, IPromotionCodeRepository
    {
        public PromotionCodeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}