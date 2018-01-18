using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Permisstion : IEntityBase {
        public int Id { get; set; }
        public int? GroupNews { get; set; }
        public int? UserId { get; set; }
        public int? Permiss { get; set; }
    }
}
