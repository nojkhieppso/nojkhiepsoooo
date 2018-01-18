
using HomeCinema.Entities;


namespace HomeCinema.Data.Configurations {
    
    
    public class DonghuongConfiguration : EntityBaseConfiguration<Donghuong> {
        
        public DonghuongConfiguration() {
			Property(x => x.Id).IsRequired();
			Property(x => x.Name).IsRequired();
			Property(x => x.Ngaysinh);
			Property(x => x.Xom).IsRequired();
			Property(x => x.Congviec).IsRequired();
			Property(x => x.Diachi).IsRequired();
			Property(x => x.Dienthoai).IsRequired();
			Property(x => x.Email).IsRequired();
			Property(x => x.Acount).IsRequired();
			Property(x => x.Pass).IsRequired();
			Property(x => x.GroupNewsId);
			Property(x => x.Date);
        }
    }
}
