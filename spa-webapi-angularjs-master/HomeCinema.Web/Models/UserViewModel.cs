using HomeCinema.Entities;
using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models
{
    public class UserViewModel 
    {
        public UserViewModel()
        {
            UserRoles = new List<UserRole>();
            GroupUser = new List<GroupUser>();
            Profiler = new List<Profiler>();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<Profiler> Profiler { get; set; }
    }
}