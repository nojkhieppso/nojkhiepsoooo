using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    public class Group : IEntityGuid
    {
        public Group()
        {
            GroupUsers = new List<GroupUser>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<GroupUser> GroupUsers { get; set; }
    }
}
