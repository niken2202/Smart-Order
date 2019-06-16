﻿using Data.Infrastructure;
using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface IErrorService
    {
        Error Create(Error error);
        void Save();
    }

    public class ErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Error Create(Error error)
        {
            return _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}