using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IMaterialServie
    {
        Material Add(Material material);

        IEnumerable<Material> GetAll(int pageIndex, int pageSize, out int totalRow);
        IEnumerable<Material> GetAll();

        Material GetById(int id);

        Material Delete(int id);

        void Update(Material material);

        IEnumerable<Material> GetByName(string name);

        void SaveChanges();
    }

    public class MaterialService : IMaterialServie
    {
        private IUnitOfWork unitOfWork;
        private IMaterialRepository materialRepository;

        public MaterialService(IUnitOfWork unitOfWork, IMaterialRepository materialRepository)
        {
            this.unitOfWork = unitOfWork;
            this.materialRepository = materialRepository;
        }

        public Material Add(Material material)
        {
           return materialRepository.Add(material);
        }

        public Material Delete(int id)
        {
            return materialRepository.Delete(id);
        }

        public IEnumerable<Material> GetAll(int pageIndex, int pageSize, out int totalRow)
        {
            return materialRepository.GetAll( pageIndex,  pageSize, out totalRow);
        }

        public IEnumerable<Material> GetAll()
        {
            return materialRepository.GetAll();
        }

        public Material GetById(int id)
        {
            return materialRepository.GetSingleById(id);
        }

        public IEnumerable<Material> GetByName(string name)
        {
            return null;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(Material material)
        {
            materialRepository.Update(material);
        }
    }
}