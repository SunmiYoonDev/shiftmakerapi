using Dayoff.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public interface IAuthRepository
    {
        User FindUserByEmail(string email);
        bool IsActiveUser(string email);
        User Login(string email, string password);
        void Register(User user, Company company, string password);
        string ForgotPassword(string email);
    }
}
