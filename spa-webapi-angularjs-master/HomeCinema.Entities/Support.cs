using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Support : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public int? Type { get; set; }
        public string Nick { get; set; }
        public int? Ord { get; set; }
        public int? Active { get; set; }
        public int? PageId { get; set; }
        public string Lang { get; set; }
    }
}
