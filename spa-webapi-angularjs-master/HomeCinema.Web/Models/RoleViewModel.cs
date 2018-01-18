using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Models
{
    public class UserRoleViewModel
    {
        
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
    }
}