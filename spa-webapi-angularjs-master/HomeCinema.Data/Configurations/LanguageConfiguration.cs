
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class LanguageConfiguration : EntityBaseConfiguration<Language> {
        
        public LanguageConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Folder).IsRequired();
			Property(x => x.Default).IsRequired();
			Property(x => x.Image).IsRequired();
			Property(x => x.Active).IsRequired();
        }
    }
}
