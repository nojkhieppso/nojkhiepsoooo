using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class LessionConfiguration : EntityGuidConfiguration<Lession> {
        
        public LessionConfiguration() {
			Property(x => x.StartAt);
			Property(x => x.EndAt);
			Property(x => x.Money);
        }
    }
}
