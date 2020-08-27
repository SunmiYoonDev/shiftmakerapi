using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dayoff.DAL.Models.Dto
{
    public class UserForEditResource
    {
        [Required]
        public Guid id { get; set; }
        [StringLength(30, ErrorMessage = "You must specify firstname between 1 and 30 characters")]
        public string firstName { get; set; }
        [StringLength(30, ErrorMessage = "You must specify lastname between 1 and 30 characters")]
        public string lastName { get; set; }
    }
}
