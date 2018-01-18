using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class GroupViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string GroupId { get; set; }
        public List<GroupUser> GroupUser { get; set; }
    }
}