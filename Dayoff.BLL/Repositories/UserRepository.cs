using Dayoff.BLL.Services;
using Dayoff.DAL;
using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDBConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
        public User FindUserById(Guid userId)
        {
            var SqlQuery = @"SELECT *
                             FROM users
                             WHERE id= @userId";

            var user = QueryFirstOrDefault<User>(SqlQuery, new { userId });

            return user;

        }
        public User FindUserByEmail(string email)
        {
            var SqlQuery = @"SELECT *
                             FROM users
                             WHERE email= @email";

            var user = QueryFirstOrDefault<User>(SqlQuery, new { email });

            return user;
        }
        public void Edit(UserForEditResource userProfile)
        {
            var Today = String.Format("{0:s}", DateTime.Now);
            var SqlQuery = @"Update users 
                            SET first_name =@firstName, last_name=@lastName, date_changed = @dateChanged 
                            WHERE id= @userId";

            var result = Execute(SqlQuery, new
            {
                userProfile.firstName,
                userProfile.lastName,
                UserId = userProfile.id,
                DateChanged = Today
            });


        }

        public void Delete(Guid userId)
        {

            var SqlQuery = @"UPDATE users
                        SET is_deleted = true
                        WHERE id=@userId";
            Execute(SqlQuery,new { userId });

        }
    }
}
