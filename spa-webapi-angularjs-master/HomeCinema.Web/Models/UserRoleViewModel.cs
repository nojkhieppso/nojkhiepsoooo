using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Models
{
    public class RoleViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
    }
}