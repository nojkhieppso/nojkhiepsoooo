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
    public class RolePermission : IEntityBase
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
