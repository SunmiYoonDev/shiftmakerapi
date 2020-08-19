using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.BLL.Repositories.Helpers
{
    public interface ISendingEmail
    {
        void SendEmail(Guid? id, string key, string email, string function);
    }
}
