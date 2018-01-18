using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class Key_Sys_Unit : IEntityBase
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Encodedomain { get; set; }
        public int UnitID { get; set; }
    }
}
