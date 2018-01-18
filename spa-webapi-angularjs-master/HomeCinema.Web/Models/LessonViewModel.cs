using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models {
    
    public class LessionViewModel
    {
        public int Id { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public double? Money { get; set; }
        public string Description { get; set; }
        
    }
}
