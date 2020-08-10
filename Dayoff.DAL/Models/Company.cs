using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.DAL.Models
{
    public class Company
    {
        public Guid id { get; set; }
        public IFormFile thumbnail { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Guid createdBy { get; set; }
        public DateTime? dateCreated { get; set; } = null;
        public DateTime? dateModified { get; set; } = null;
        public bool isDeleted { get; set; }
    }
}
