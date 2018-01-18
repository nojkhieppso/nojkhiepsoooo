using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class Sys_UnitConfiguration: EntitySys_UnitConfiguration<Sys_Unit>
    {
        public Sys_UnitConfiguration()
        {
            Property(u => u.Website).IsRequired();
            Property(u => u.Note).IsRequired();
        }
    }
}
