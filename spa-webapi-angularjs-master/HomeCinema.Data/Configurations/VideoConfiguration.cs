
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class VideoConfiguration : EntityBaseConfiguration<Video> {
        
        public VideoConfiguration() {
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
			Property(x => x.Active);
			Property(x => x.GroupVideoId);
			Property(x => x.Lang).IsRequired();
        }
    }
}
