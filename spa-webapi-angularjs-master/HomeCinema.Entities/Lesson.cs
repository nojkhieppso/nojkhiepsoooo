using System;



namespace HomeCinema.Entities {
    
    public class Lession: IEntityGuid
    {
        public Guid Id { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public double? Money { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? Delete { get; set; }
    }
}
