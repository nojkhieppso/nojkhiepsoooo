using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Link : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public int? Position { get; set; }
        public int? Ord { get; set; }
        public int? Active { get; set; }
        public string Lang { get; set; }
    }
}
