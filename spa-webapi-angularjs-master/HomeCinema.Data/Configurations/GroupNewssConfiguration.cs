
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class GroupNewssConfiguration : EntityBaseConfiguration<GroupNewss> {
        
        public GroupNewssConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Tag).IsRequired();
			Property(x => x.Level).IsRequired();
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Index);
			Property(x => x.Lang).IsRequired();
        }
    }
}
