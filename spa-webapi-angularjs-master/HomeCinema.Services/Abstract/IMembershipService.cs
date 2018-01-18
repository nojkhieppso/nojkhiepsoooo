using HomeCinema.Entities;
using HomeCinema.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Services
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string email, string password, Guid[] roles);
        User CreateUser(string username, string email, string password);
        User GetUser(Guid userId);
        List<Role> GetUserRoles(string username);
    }
}
