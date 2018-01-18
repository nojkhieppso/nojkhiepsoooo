using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Tbvisistor : IEntityBase {
        public int Id { get; set; }
        public int? Iuseronline { get; set; }
        public long? Ivisistor { get; set; }
        public int? Iuseronlinedate { get; set; }
        public DateTime? Dateonline { get; set; }
    }
}
