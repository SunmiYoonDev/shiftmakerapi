using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class UserForLoginResource
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
