using Common.Exceptions;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IApplicationRoleService
    {
        IEnumerable<ApplicationRole> GetAll();
        ApplicationRole Add(ApplicationRole appRole);
        IEnumerable<string> GetRoleByUserID(string id);
        void SaveChanges();
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository applicationRoleRepository;
        private IUnitOfWork unitOfWork;

        public ApplicationRoleService(  IUnitOfWork unitOfWork ,IApplicationRoleRepository applicationRoleRepository)
        {
            this.applicationRoleRepository = applicationRoleRepository;
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<ApplicationRole> GetAll()
        {
            return applicationRoleRepository.GetAllRoles();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (applicationRoleRepository.CheckContains(x => x.Name == appRole.Name))
                throw new NameDuplicatedException("Tên không được trùng");
            return applicationRoleRepository.Add(appRole);
        }

        public IEnumerable<string> GetRoleByUserID(string id)
        {
            return applicationRoleRepository.GetRoleByUserID(id);
        }
    }
}