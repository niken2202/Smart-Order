using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System.Collections.Generic;

namespace Service
{
    public interface IBillDetailService
    {
        IEnumerable<BillDetail> GetByBillId(int id);
        void SaveChanges();
    }
    public class BillDetailService : IBillDetailService
    {
        IBillDetailRepository billDetailRepository;
        IUnitOfWork unitOfWork;

        public BillDetailService(IBillDetailRepository billDetailRepository, IUnitOfWork unitOfWork)
        {
            this.billDetailRepository = billDetailRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<BillDetail> GetByBillId(int id)
        {
            var listDishOfBill = billDetailRepository.GetByBillID(id);
            return listDishOfBill;
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
