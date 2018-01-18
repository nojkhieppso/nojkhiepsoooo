using System;
using System.Collections.Generic;


namespace HomeCinema.Web.Models {
    
    public class LessionaddViewModel
    {
        public Guid Id { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public double? Money { get; set; }
        public string Description { get; set; }
        
    }
}
