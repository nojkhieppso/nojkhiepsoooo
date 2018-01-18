using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class CalendarConfiguration : EntityGuidConfiguration<Calendar>
    {
        public CalendarConfiguration()
        {
            Property(u => u.Description).IsRequired().HasMaxLength(100);
            Property(u => u.UserID).IsRequired();
            Property(u => u.StartAt).IsRequired();
            Property(u => u.EndAt).IsRequired();
        }
    }
}
