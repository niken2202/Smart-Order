using Common.ViewModels;
using Data.Infrastructure;
using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;

namespace Service
{
    public interface IBillService
    {
        Bill Add(Bill bill);

        IEnumerable<Bill> GetAll();

        Bill GetById(int id);

        IEnumerable<Bill> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class BillService : IBillService
    {
        private IUnitOfWork unitOfWork;
        private IBillRepository billRepository;

        public BillService(IUnitOfWork unitOfWork, IBillRepository billRepository)
        {
            this.unitOfWork = unitOfWork;
            this.billRepository = billRepository;
        }

        public Bill Add(Bill bill)
        {
           return  billRepository.Add(bill);
        }

        public IEnumerable<Bill> GetAll()
        {
            return billRepository.GetAll();
        }

        public IEnumerable<Bill> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return billRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize, new string[] { "Bill" });

        }

        public Bill GetById(int id)
        {
            return billRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}