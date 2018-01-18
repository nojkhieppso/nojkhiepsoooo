
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class PerLibraryConfiguration : EntityBaseConfiguration<PerLibrary> {
        
        public PerLibraryConfiguration() {
			Property(x => x.GroupImageId);
			Property(x => x.UserId);
			Property(x => x.Permiss);
        }
    }
}
