using Dayoff.BLL.Services;
using Dayoff.DAL;
using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public class CompanyRepository: BaseRepository, ICompanyRepository
    {
        public CompanyRepository(IDBConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public IEnumerable<Company> GetList(Guid userId)
        {
            var SqlQuery = @"SELECT A.*
                            FROM companies A
		                    JOIN enrolment B ON A.id = B.company_id
                            WHERE B.user_id= @userId
                            ORDER BY B.date_added";
            var courses = Query<Company>(SqlQuery, new { userId });
            return courses;
        }
        public void Add(Guid userId, CompanyForAddResource companyForAddDto)
        {
            var Today = String.Format("{0:s}", DateTime.Now);

            var companyId = Guid.NewGuid();

            var SqlQuery = @"INSERT INTO companies (id, name, description, created_by, date_created)
                            VALUES (@companyId, @name, @description, @userId, @Today)";

            Execute(SqlQuery, new
            {
                companyId,
                companyForAddDto.name,
                companyForAddDto.description,
                userId,
                Today
            });

            SqlQuery = @"INSERT INTO enrolment (user_id, company_id, is_active, date_added)
                            VALUES (@userId, @companyId, 1, @Today)";

            Execute(SqlQuery, new
            {
                userId,
                companyId,
                Today
            });

        }
    }
}
