using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class MemberConfiguration : EntityBaseConfiguration<Member> {
        
        public MemberConfiguration() {
			Property(x => x.Id).IsRequired();
			Property(x => x.Name).IsRequired();
			Property(x => x.Email).IsRequired();
			Property(x => x.Username).IsRequired();
			Property(x => x.Password).IsRequired();
			Property(x => x.Active);
        }
    }
}
