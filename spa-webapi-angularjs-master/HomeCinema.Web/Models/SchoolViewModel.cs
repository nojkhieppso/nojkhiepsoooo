using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models {
    
    public class SchoolViewModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? Delete { get; set; }
    }
}
