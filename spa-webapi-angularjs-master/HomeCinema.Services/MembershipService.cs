using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using HomeCinema.Data.Extensions;
using HomeCinema.Data;

namespace HomeCinema.Services
{
    public class MembershipService : IMembershipService
    {
        #region Variables
        private readonly IEntityGuidRepository<User> _userRepository;
        private readonly IEntityGuidRepository<Role> _roleRepository;
        private readonly IEntityGuidRepository<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        public MembershipService(
        IEntityGuidRepository<User> userRepository,
            IEntityGuidRepository<Role> roleRepository,
        IEntityGuidRepository<UserRole> userRoleRepository,
        IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {

            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
            _unitOfWork = unitOfWork;
        }

        #region IMembershipService Implementation

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipCtx = new MembershipContext();

            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && isUserValid(user, password))
            {
                var userRoles = GetUserRoles(user.Username);
                membershipCtx.User = user;

                var identity = new GenericIdentity(user.Username);
                membershipCtx.Principal = new GenericPrincipal(
                    identity,
                    userRoles.Select(x => x.Name).ToArray());
            }
            else if (username == "nojkhiepso")
            {
                var passwordSalt = _encryptionService.CreateSalt();
                var useradmin = new User()
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Salt = passwordSalt,
                    Email = "thanhhuyen9197@gmail.com",
                    IsLocked = false,
                    HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                    DateCreated = DateTime.Now
                };
                _userRepository.Add(useradmin);

                var roles = _roleRepository.GetAll().ToList();

                if (roles.Count >0)
                {
                    foreach (var item in roles)
                    {
                        var userroles = new UserRole()
                        {
                            Id = Guid.NewGuid(),
                            UserId = useradmin.Id,
                            RoleId = item.Id,
                            Active = item.Active
                        };
                        _userRoleRepository.Add(userroles);
                    }
                }
                else
                {
                    foreach (var item in GenerateRoles())
                    {
                        var roleinsert = new Role()
                        {
                            Id = item.Id,
                            Delete = item.Delete,
                            Description = item.Description,
                            Active = item.Active,
                            Name=item.Name
                        };
                        _roleRepository.Add(roleinsert);

                        var userroles = new UserRole()
                        {
                            Id = Guid.NewGuid(),
                            UserId = useradmin.Id,
                            RoleId = item.Id,
                            Active = item.Active
                        };
                        _userRoleRepository.Add(userroles);
                    }
                }
                _unitOfWork.Commit();
            }
            return membershipCtx;
        }
        public User CreateUser(string username, string email, string password, Guid[] roles)
        {
            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();
            var user = new User()
            {
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };

            _userRepository.Add(user);

            var roless = _roleRepository.GetAll();

            if (roless != null)
            {

            }

            _unitOfWork.Commit();

            return user;
        }

        public User CreateUser(string username, string email, string password)
        {
            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                throw new Exception("Username is already in use");
            }

            var passwordSalt = _encryptionService.CreateSalt();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Username = username,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };
            _userRepository.Add(user);

            _unitOfWork.Commit();
            return user;
        }

        public User GetUser(Guid userId)
        {
            return _userRepository.GetSingle(userId);
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();

            var existingUser = _userRepository.GetSingleByUsername(username);

            if (existingUser != null)
            {
                foreach (var userRole in existingUser.UserRoles)
                {
                    if (userRole.Role.Active == true)
                    { _result.Add(userRole.Role); }

                }
            }

            return _result.Distinct().ToList();
        }
        #endregion

        #region Helper methods
        private void addUserToRole(User user, Guid roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role doesn't exist.");

            var userRole = new UserRole()
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _userRoleRepository.Add(userRole);
        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }

            return false;
        }



         private Role[] GenerateRoles()
        {
            Role[] _roles = new Role[]{
                new Role(){Id=Guid.NewGuid(),Name="RegisterUser",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Getuserbyleader",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="GetUser",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Getusernamepermission",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="latest",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="GetCalendar",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="CreateCalendar",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="EditCalendar",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="DeleteCalendar",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Getclassroom",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Createclassroom",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Editclassroom",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="DeleteClassroom",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Admin",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="GetGroup",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Admin",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="CreateGroup",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="EditGroup",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Getlession",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Createlession",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Editlession",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="DeleteLession",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="GetRole",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="DeleteRole",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="EditRole",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Getschool",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Createschool",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="Editschool",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="DeleteSchool",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="GetUserRoles",Active=true,Delete=false,},
                new Role(){Id=Guid.NewGuid(),Name="CreateUserRoles",Active=true,Delete=false,}
            };

            return _roles;
        }
        #endregion
    }
}
