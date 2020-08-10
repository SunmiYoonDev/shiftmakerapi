using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class UserForRegisterResource
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email is not valid")]
        public string email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "You must specify a password between 8 and 25 characters")]
        public string password { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        [Required]
        public string companyName { get; set; }
    }
}
