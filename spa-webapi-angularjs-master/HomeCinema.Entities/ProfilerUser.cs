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
    public class ProfilerUser 
    {
        public int ProfilerId { get; set; }
        public int UserID { get; set; }
        public virtual Profiler Profiler { get; set; }
    }
}
