using CodesApp.Model;
using CodesApp.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodesApp.Service.Interfaces
{
   public interface ICodesService
    {

        public Task<int> Add(CodeModel model);

        public Task<int> Update(int codeId, CodeModel model);

        public Task<int> ActivateDeactivate(int codeId, bool isActive);

        public void Delete(int codeId);

        public Task<List<Code>> GetAllCodes();
      
    }
}