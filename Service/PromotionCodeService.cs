using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IPromotionCodeService
    {
        PromotionCode Add(PromotionCode promotionCode);
        IEnumerable<PromotionCode> GetAll();
        PromotionCode Delete(int id);
        void Update(PromotionCode promotionCode);
        PromotionCode CheckValid(string Code);
        PromotionCode GetByCode(string Code);
        void SaveChanges();
    }

    public class PromotionCodeService : IPromotionCodeService
    {
        private IUnitOfWork unitOfWork;
        private IPromotionCodeRepository promotionCodeRepository;

        public PromotionCodeService(IUnitOfWork unitOfWork, IPromotionCodeRepository promotionCodeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.promotionCodeRepository = promotionCodeRepository;
        }

        public PromotionCode Add(PromotionCode promotionCode)
        {
            return promotionCodeRepository.Add(promotionCode);
        }

        public PromotionCode GetByCode(string Code)
        {
            return promotionCodeRepository.GetSingleByCondition(x => x.Code.Trim().Equals(Code.Trim()) );
        }

        public PromotionCode Delete(int id)
        {
            return promotionCodeRepository.Delete(id);
        }

        public IEnumerable<PromotionCode> GetAll()
        {
            return promotionCodeRepository.GetAll();
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }

        public void Update(PromotionCode promotionCode)
        {
            promotionCodeRepository.Update(promotionCode);
        }

        public PromotionCode CheckValid(string Code)
        {
            return promotionCodeRepository.GetSingleByCondition(x => x.Status == true &&
            x.Code.Trim().Equals(Code.Trim())
            && (DateTime.Compare(x.ExpiredDate,DateTime.Now)>=0)
            );
        }
    }
}