﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    /// <summary>
    /// HomeCinema User's Role
    /// </summary>
    public class GroupUser : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public virtual Group Groups { get; set; }
        public virtual User User { get; set; }
    }
}
