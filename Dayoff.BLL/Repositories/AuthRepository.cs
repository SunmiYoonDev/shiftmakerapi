using Dayoff.BLL.Repositories.Helpers;
using Dayoff.BLL.Services;
using Dayoff.DAL;
using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        private readonly ISendingEmail _emailFactory;
        public AuthRepository(IDBConnectionFactory connectionFactory, ISendingEmail emailFactory) : base(connectionFactory)
        {
            _emailFactory = emailFactory;
        }

        public User FindUserByEmail(string email)
        {
            var SqlQuery = @"SELECT *
                             FROM users
                             WHERE email= @email";

            var user = QueryFirstOrDefault<User>(SqlQuery, new { email });

            return user;

        }
        public bool IsActiveUser(string email)
        {
            User activeUser = FindUserByEmail(email);
            if (!activeUser.isActive)
                return false;
            return true;
        }
        public User Login(string email, string password)
        {
            var loginUser = FindUserByEmail(email);

            if (!VerifyPasswordHash(password, Convert.FromBase64String(loginUser.passwordHash), Convert.FromBase64String(loginUser.passwordSalt)))
                return null;

            var Today = String.Format("{0:s}", DateTime.Now);

            var SqlQuery = "";

            SqlQuery = @"UPDATE users
                             SET last_login = @Today
                            WHERE email= @email";
            Execute(SqlQuery,new { Today, email });

            return loginUser;

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public void Register(User user, Company company, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.passwordHash = Convert.ToBase64String(passwordHash);
            user.passwordSalt = Convert.ToBase64String(passwordSalt);

            var Today = String.Format("{0:s}", DateTime.Now);

            var SqlQuery = @"INSERT INTO users (id, email, password_hash, password_salt, first_name, last_name, is_admin, is_active, date_activated, date_added, date_changed)
                       VALUES (@id, @email, @passwordHash, @passwordSalt, @firstName, @lastName, @isAdmin, @isActive,@Today, @Today, @Today)";

            Execute(SqlQuery,new { user.id, user.email, user.passwordHash, user.passwordSalt, user.firstName, user.lastName,
                user.isAdmin, user.isActive, Today });

            CreateCompany(user.id, company);
            EnrollCompany(user.id, company.id);

        }

        private void CreateCompany(Guid adminId, Company company)
        {
            var Today = String.Format("{0:s}", DateTime.Now);

            var SqlQuery = @"INSERT INTO companies (id, name, created_by, date_created)
                            VALUES (@id, @name, @adminId, @Today)";

            Execute(SqlQuery, new
            {
                company.id,
                company.name,
                adminId,
                Today
            });

        }
        private void EnrollCompany(Guid adminId, Guid companyId)
        {
            var Today = String.Format("{0:s}", DateTime.Now);

            var SqlQuery = @"INSERT INTO enrolment (user_id, company_id, is_active, date_added)
                            VALUES (@adminId, @companyId, 1, @Today)";

            Execute(SqlQuery, new
            {
                adminId,
                companyId,
                Today
            });
        }

        public string ForgotPassword(string email)
        {
            var forgotUser = FindUserByEmail(email);

            forgotUser.validKey = Guid.NewGuid().ToString();

            var Today = String.Format("{0:s}", DateTime.Now);

            var SqlQuery = @"UPDATE users
                             SET valid_key = @validKey," +
                            " date_changed = @Today" +
                            " WHERE email = @email";
            Execute(SqlQuery, new { forgotUser.validKey, Today, email });

            //_emailFactory.SendEmail(forgotUser.id, forgotUser.validKey, email, "forgotpassword");


            //SqlQuery = @"INSERT INTO email (mail_to, new_message, date_added, date_sent, type_id)
            //            VALUES (@email, 'ForgotPassword', @Today, @Today,3)";

            //var result = Execute(SqlQuery, new {  Today, email });

            return email;
        }
    }
}
