using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public class CompanyRepository
    {
        public void Add(Guid companyId, Guid adminId, CompanyForAddResource companyForAddDto)
        {
            var today = String.Format("{0:s}", DateTime.Now);



        }
    }
}
