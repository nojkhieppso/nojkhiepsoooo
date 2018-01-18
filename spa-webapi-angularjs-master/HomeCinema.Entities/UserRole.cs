using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    /// <summary>
    /// HomeCinema User's Role
    /// </summary>
    public class UserRole : IEntityGuid
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public bool Active { get; set; }
    }
}
