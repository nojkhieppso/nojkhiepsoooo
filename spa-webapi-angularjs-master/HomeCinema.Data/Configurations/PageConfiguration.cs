
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class PageConfiguration : EntityBaseConfiguration<Page> {
        
        public PageConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Tag).IsRequired();
			Property(x => x.Detail).IsRequired();
			Property(x => x.Level).IsRequired();
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Type);
			Property(x => x.Link).IsRequired();
			Property(x => x.Target).IsRequired();
			Property(x => x.Position);
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
			HasMany(x => x.Advertise).WithRequired().HasForeignKey(s => s.PageId);
        }
    }
}
