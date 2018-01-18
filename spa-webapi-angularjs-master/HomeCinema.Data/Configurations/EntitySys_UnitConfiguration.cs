using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class EntitySys_UnitConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntitySys_Unit
    {
        public EntitySys_UnitConfiguration()
        {
            HasKey(e => e.UnitID);
        }
    }
}
