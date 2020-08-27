using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.DAL.Models
{
    public class User
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public DateTime dateActivated { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateChanged { get; set; }
        public DateTime lastLogin { get; set; }
        public int titleId { get; set; }
        public int positionId { get; set; }
        public string validKey { get; set; }
        public bool isDeleted { get; set; }

    }
}
