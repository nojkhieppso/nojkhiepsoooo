
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class DocumentTypeConfiguration : EntityBaseConfiguration<DocumentType> {
        
        public DocumentTypeConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
			Property(x => x.Image).IsRequired();
			HasMany(x => x.Document).WithRequired().HasForeignKey(s => s.TypeId);
        }
    }
}
