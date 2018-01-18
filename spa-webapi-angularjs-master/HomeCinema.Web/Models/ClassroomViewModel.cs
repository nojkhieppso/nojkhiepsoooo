using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models {
    
    public class ClassroomViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public bool? Delete { get; set; }
        public string Descriptions { get; set; }
        public int? Totalstudent { get; set; }
    }
}
