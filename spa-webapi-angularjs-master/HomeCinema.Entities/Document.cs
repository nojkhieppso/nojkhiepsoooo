using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Document : IEntityBase {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Info { get; set; }
        public string File { get; set; }
        public int? Priority { get; set; }
        public int? Active { get; set; }
        public string Lang { get; set; }
    }
}
