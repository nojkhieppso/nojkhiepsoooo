
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class ContactConfiguration : EntityBaseConfiguration<Contact> {
        
        public ContactConfiguration() {
			Property(x => x.Name).IsRequired();
			Property(x => x.Company).IsRequired();
			Property(x => x.Address).IsRequired();
			Property(x => x.Tel).IsRequired();
			Property(x => x.Mail).IsRequired();
			Property(x => x.Detail).IsRequired();
			Property(x => x.Date);
			Property(x => x.Active);
			Property(x => x.Lang).IsRequired();
        }
    }
}
