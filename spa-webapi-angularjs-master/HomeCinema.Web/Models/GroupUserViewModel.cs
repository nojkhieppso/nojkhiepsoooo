using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using HomeCinema.Entities;

namespace HomeCinema.Web.Models
{
    public class GroupUserViewModel
    {
        public int ID { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public List<GroupUser> GroupUsers { get; set; } 
    }
}