using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetList(Guid userId);
        void Add(Guid userId, CompanyForAddResource companyForAddDto);
    }
}
