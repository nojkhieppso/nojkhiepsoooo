using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class ConfigConfiguration : EntityBaseConfiguration<Config> {
        
        public ConfigConfiguration() {
			Property(x => x.MailSmtp).IsRequired().HasMaxLength(64);
			Property(x => x.MailPort).IsRequired();
			Property(x => x.MailInfo).IsRequired().HasMaxLength(64);
			Property(x => x.MailNoreply).IsRequired().HasMaxLength(64);
			Property(x => x.MailPassword).IsRequired().HasMaxLength(64);
			Property(x => x.PlaceHead).IsRequired();
			Property(x => x.PlaceBody).IsRequired();
			Property(x => x.GoogleId).IsRequired();
			Property(x => x.Contact).IsRequired();
			Property(x => x.Copyright).IsRequired();
			Property(x => x.Title).IsRequired();
			Property(x => x.Description).IsRequired();
			Property(x => x.Keyword).IsRequired();
			Property(x => x.Lang).IsRequired();
        }
    }
}
