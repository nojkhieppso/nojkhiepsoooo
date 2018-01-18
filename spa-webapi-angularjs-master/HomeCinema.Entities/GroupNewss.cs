using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class GroupNewss : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Level { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public int? Ord { get; set; }
        public int? Active { get; set; }
        public int? Index { get; set; }
        public string Lang { get; set; }
    }
}
