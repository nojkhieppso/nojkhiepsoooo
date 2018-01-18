using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class Key_Sys_UnitConfiguration : EntityBaseConfiguration<Key_Sys_Unit>
    {
        public Key_Sys_UnitConfiguration()
        {
            Property(ur => ur.Key).IsRequired();
            Property(ur => ur.UnitID).IsRequired();
            Property(ur => ur.Encodedomain).IsRequired();
        }
    }
}
