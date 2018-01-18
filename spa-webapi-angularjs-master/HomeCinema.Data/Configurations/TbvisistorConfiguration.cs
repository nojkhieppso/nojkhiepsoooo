
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class TbvisistorConfiguration : EntityBaseConfiguration<Tbvisistor> {
        
        public TbvisistorConfiguration() {
			Property(x => x.Iuseronline);
			Property(x => x.Ivisistor);
			Property(x => x.Iuseronlinedate);
			Property(x => x.Dateonline);
        }
    }
}
