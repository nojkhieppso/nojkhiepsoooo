using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class DocumentType : IEntityBase {
        public DocumentType() {
			Document = new List<Document>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Ord { get; set; }
        public int? Active { get; set; }
        public string Lang { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Document> Document { get; set; }
    }
}
