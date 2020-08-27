using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class UserForAddResource
    {
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        [StringLength(30, ErrorMessage = "You must specify firstname between 1 and 30 characters")]
        public string firstName { get; set; }
        [StringLength(30, ErrorMessage = "You must specify lastname between 1 and 30 characters")]
        public string lastName { get; set; }
    }
}
