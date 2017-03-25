using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BazinamSite2.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string nameAndFamily { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public bool isActive { get; set; }
        public DateTime lastLogin { get; set; }
        public virtual Role roleModel { get; set; }

    }

    public class Role
    {
        public int RoleID { get; set; }
        public string roleName { get; set; }
        public string description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}