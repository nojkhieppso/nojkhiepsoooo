
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class AdvertiseConfiguration : EntityBaseConfiguration<Advertise> {
        
        public AdvertiseConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Image).IsRequired();
			Property(x => x.Width);
			Property(x => x.Height);
			Property(x => x.Link).IsRequired();
			Property(x => x.Target).IsRequired();
			Property(x => x.Content).IsRequired();
			Property(x => x.Position);
			Property(x => x.PageId);
			Property(x => x.Click);
			Property(x => x.Ord);
			Property(x => x.Active).IsRequired();
			Property(x => x.Lang).IsRequired();
        }
    }
}
