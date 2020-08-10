using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public interface ICompanyRepository
    {
        void Add(Guid companyId, Guid adminId, CompanyForAddResource companyForAddDto);
    }
}
