using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Advertise : IEntityBase {
        public int Id { get; set; }
        public int PageId { get; set; }
        public virtual Page Page { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Link { get; set; }
        public string Target { get; set; }
        public string Content { get; set; }
        public short? Position { get; set; }
        public int? Click { get; set; }
        public int? Ord { get; set; }
        public bool Active { get; set; }
        public string Lang { get; set; }
    }
}
