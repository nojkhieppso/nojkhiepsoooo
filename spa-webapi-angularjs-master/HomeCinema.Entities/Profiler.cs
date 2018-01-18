using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities
{
    /// <summary>
    /// HomeCinema Customer Info
    /// </summary>
    public class Profiler : IEntityGuid
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool? Active { get; set; }
        public Guid? UserId { get; set; }
        public string Color { get; set; }
    }
}
