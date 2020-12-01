using CodesApp.Infrastructure.Data;
using CodesApp.Model;
using CodesApp.Service.Interfaces;
using CodesApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodesApp.Service
{
    public class CodesService :ICodesService
    {
        private readonly IRepository<Code> _codeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CodesService(IRepository<Code> codeRepository,
            IUnitOfWork unitOfWork)
        {
            _codeRepository = codeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<int> Add(CodeModel model)
        {
            try
            {
                var code = new Code { CodeValue = model.CodeValue, SoftwareName = model.SoftwareName, LastModified = DateTime.Now, IsActive = model.IsActive };
                _codeRepository.Add(code);
                _unitOfWork.SaveChanges();
                return Task.FromResult(code.Id);
            }
            catch 
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> Update(int codeId, CodeModel model)
        {
            try
            {
                var code = _codeRepository.FindById(codeId);
                code.CodeValue = model.CodeValue;
                code.SoftwareName = model.SoftwareName;
                code.LastModified = DateTime.Now;
                code.IsActive = model.IsActive;
                _unitOfWork.SaveChanges();
                return Task.FromResult(code.Id);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public Task<int> ActivateDeactivate(int codeId, bool isActive)
        {
            try
            {
                var code = _codeRepository.FindById(codeId);
                code.LastModified = DateTime.Now;
                code.IsActive = isActive;
                _unitOfWork.SaveChanges();
                return Task.FromResult(code.Id);
            }
            catch
            {
                return Task.FromResult(0);
            }
        }

        public void Delete(int codeId)
        {
                _codeRepository.Delete(codeId);
                _unitOfWork.SaveChanges();
        }


        public async Task<List<Code>> GetAllCodes()
        {
            return await Task.FromResult(_codeRepository.QueryAll().ToList());
        }

    }
}