using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.DAL.Models
{
    public class Enrolment
    {
        public int id { get; set; }
        public Guid userId { get; set; }
        public Guid companyId { get; set; }
        public bool isActive { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateFinished { get; set; }
    }
}
