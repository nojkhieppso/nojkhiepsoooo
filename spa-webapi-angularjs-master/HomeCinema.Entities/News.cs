using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class News : IEntityBase {
        public int Id { get; set; }
        public int GroupNewsId { get; set; }
        public virtual GroupNews GroupNews { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
        public string File { get; set; }
        public string Content { get; set; }
        public string Detail { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public int? Priority { get; set; }
        public int? Index { get; set; }
        public int? Check1 { get; set; }
        public int? Check2 { get; set; }
        public int? Check3 { get; set; }
        public int? Check4 { get; set; }
        public int? Check5 { get; set; }
        public int? Check6 { get; set; }
        public int? Order { get; set; }
        public int? Active { get; set; }
        public string Lang { get; set; }
        public float? Views { get; set; }
    }
}
