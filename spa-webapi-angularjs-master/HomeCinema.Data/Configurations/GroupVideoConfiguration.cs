
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class GroupVideoConfiguration : EntityBaseConfiguration<GroupVideo> {
        
        public GroupVideoConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Tag).IsRequired();
			Property(x => x.Level).IsRequired();
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
			HasMany(x => x.Video).WithRequired().HasForeignKey(s => s.GroupVideoId);
        }
    }
}
