using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations
{


    public class LinkConfiguration : EntityBaseConfiguration<Link> {
        
        public LinkConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Line1).IsRequired();
			Property(x => x.Line2).IsRequired();
			Property(x => x.Link1).IsRequired();
			Property(x => x.Link2).IsRequired();
			Property(x => x.Position);
			Property(x => x.Ord);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
        }
    }
}
