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
    public class Role : IEntityGuid
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public bool? Delete { get; set; }
        public string Menu { get; set; }
        public string Link { get; set; }
        public int? Lang { get; set; }
    }
}
