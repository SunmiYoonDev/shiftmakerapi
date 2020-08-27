using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class UserForForgotPasswordResource
    {
        [Required]
        public string email { get; set; }

    }
}
