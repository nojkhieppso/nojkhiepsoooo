
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class SupportConfiguration : EntityBaseConfiguration<Support> {
        
        public SupportConfiguration() {
			Property(x => x.Id).IsRequired();
			Property(x => x.Name).IsRequired();
			Property(x => x.Tel).IsRequired();
			Property(x => x.Type);
			Property(x => x.Nick).IsRequired();
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.PageId);
			Property(x => x.Lang).IsRequired();
        }
    }
}
