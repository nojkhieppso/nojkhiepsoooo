using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class CalenderLession: IEntityGuid
    {
        public Guid Id { get; set; }
        public Guid? CalendarId { get; set; }
        public Guid? ClassroomId { get; set; }
        public Guid? LessionId { get; set; }
        public Guid? SchoolId { get; set; }
        public virtual Calendar Calendar { get; set; }
        public virtual Lession Lession { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual School School { get; set; }

    }
}
