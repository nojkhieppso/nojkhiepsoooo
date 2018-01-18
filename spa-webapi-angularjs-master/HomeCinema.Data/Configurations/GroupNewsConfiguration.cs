
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class GroupNewsConfiguration : EntityBaseConfiguration<GroupNews> {
        
        public GroupNewsConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Tag).IsRequired();
			Property(x => x.Level).IsRequired();
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
			Property(x => x.Index);
			HasMany(x => x.News).WithRequired().HasForeignKey(s => s.GroupNewsId);
        }
    }
}
