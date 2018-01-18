using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Video : IEntityBase {
        public int Id { get; set; }
        public int GroupVideoId { get; set; }
        public virtual GroupVideo GroupVideo { get; set; }
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
        public int? Active { get; set; }
        public string Lang { get; set; }
    }
}
