using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class GroupConfiguration : EntityGuidConfiguration<Group> 
    {
        public GroupConfiguration()
        {
            Property(m => m.Name).IsRequired().HasMaxLength(100);
        }
    }
}
