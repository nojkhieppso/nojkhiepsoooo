using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class Sys_UserConfiguration : EntitySys_UserConfiguration<Sys_User>
    {
        public Sys_UserConfiguration()
        {
            Property(u => u.UserName).IsRequired();
            
        }
    }
}
