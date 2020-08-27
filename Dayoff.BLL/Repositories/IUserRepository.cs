using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public interface IUserRepository
    {
        void Edit(UserForEditResource userProfile);
        User FindUserById(Guid userId);
        User FindUserByEmail(string email);
        void Delete(Guid userId);
    }
}
