using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Language : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public bool Default { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
    }
}
