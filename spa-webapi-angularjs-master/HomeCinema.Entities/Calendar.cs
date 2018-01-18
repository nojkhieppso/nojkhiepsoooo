using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class Calendar : IEntityGuid
    {
        public Calendar()
        {
            CalenderLession = new List<CalenderLession>();
        }
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public bool IsFullDay { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int? Soluongchau { get; set; }
        public virtual ICollection<CalenderLession> CalenderLession { get; set; }
    }
}
