using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class CompanyForAddResource
    {
        [Required]
        public string name { get; set; }
        public string description { get; set; }
    }
}
