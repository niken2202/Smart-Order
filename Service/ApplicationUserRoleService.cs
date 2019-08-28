using Data.Infrastructure;
using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface IApplicationUserRoleService
    {
        ApplicationUserRole Add(ApplicationUserRole applicationUserRole);

        void SaveChanges();
    }

    public class ApplicationUserRoleService : IApplicationUserRoleService
    {
        private IUnitOfWork unitOfWork;
        private IApplicationUserRoleRepository applicationUserRoleRepository;

        public ApplicationUserRoleService(IUnitOfWork unitOfWork, IApplicationUserRoleRepository applicationUserRoleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.applicationUserRoleRepository = applicationUserRoleRepository;
        }

        public ApplicationUserRole Add(ApplicationUserRole applicationUserRole)
        {
            return applicationUserRoleRepository.Add(applicationUserRole);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}