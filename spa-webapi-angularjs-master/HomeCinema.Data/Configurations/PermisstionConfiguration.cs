
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class PermisstionConfiguration : EntityBaseConfiguration<Permisstion> {
        
        public PermisstionConfiguration() {
			Property(x => x.GroupNews);
			Property(x => x.UserId);
			Property(x => x.Permiss);
        }
    }
}
