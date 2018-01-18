using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    /// <summary>
    /// HomeCinema Role
    /// </summary>
    public class Permission : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
