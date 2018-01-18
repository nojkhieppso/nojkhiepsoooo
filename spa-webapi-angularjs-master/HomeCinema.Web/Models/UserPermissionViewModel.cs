using HomeCinema.Entities;
using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models
{
    public class UserPermissionViewModel 
    {
        public UserPermissionViewModel()
        {
            UserRoles = new List<UserRole>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public Role Roles { get; set; }
    }
}