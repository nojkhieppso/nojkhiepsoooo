
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class NewsConfiguration : EntityBaseConfiguration<News> {
        
        public NewsConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Tag).IsRequired();
			Property(x => x.Image).IsRequired();
			Property(x => x.File).IsRequired();
			Property(x => x.Content).IsRequired();
			Property(x => x.Detail).IsRequired();
			Property(x => x.Date);
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Priority);
			Property(x => x.Index);
			Property(x => x.Check1);
			Property(x => x.Check2);
			Property(x => x.Check3);
			Property(x => x.Check4);
			Property(x => x.Check5);
			Property(x => x.Check6);
			Property(x => x.Order);
			Property(x => x.Active);
			Property(x => x.GroupNewsId);
			Property(x => x.Lang).IsRequired();
			Property(x => x.Views);
        }
    }
}
