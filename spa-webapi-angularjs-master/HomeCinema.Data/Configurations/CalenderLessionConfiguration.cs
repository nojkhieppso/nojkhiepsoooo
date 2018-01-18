using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class CalenderLessionConfiguration : EntityGuidConfiguration<CalenderLession> {
        
        public CalenderLessionConfiguration() {
			Property(x => x.CalendarId);
			Property(x => x.LessionId);
            Property(x => x.ClassroomId);
        }
    }
}
