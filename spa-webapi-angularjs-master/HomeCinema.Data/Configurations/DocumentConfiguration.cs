
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class DocumentConfiguration : EntityBaseConfiguration<Document> {
        
        public DocumentConfiguration() {
			Property(x => x.Code).IsRequired();
			Property(x => x.Name).IsRequired();
			Property(x => x.CreateDate);
			Property(x => x.EffectiveDate);
			Property(x => x.Info).IsRequired();
			Property(x => x.File).IsRequired();
			Property(x => x.Priority);
			Property(x => x.Active);
			Property(x => x.TypeId);
			Property(x => x.Lang).IsRequired();
        }
    }
}
