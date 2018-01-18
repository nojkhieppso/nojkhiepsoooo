using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Data.Extensions;

namespace HomeCinema.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/userroles")]
    public class UserRolesController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<UserRole> _userroleRepository;
        private readonly IEntityGuidRepository<Role> _roleRepository;
        private readonly IEntityGuidRepository<User> _userRepository;
        public UserRolesController(
            IEntityGuidRepository<Role> roleRepository,
            IEntityGuidRepository<User> userRepository,
            IEntityGuidRepository<UserRole> userroleRepository,
             IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _roleRepository = roleRepository;
            _userroleRepository = userroleRepository;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "GetUserRoles")]
        [Route("search/{page:int=0}/{pageSize=4}/{userid=0}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, Guid userid, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;

                var user = _userRepository.GetSingle(userid);
                if(user!=null)
                {
                    #region[Getuserroles]
                    List<UserRole> userroles = null;
                    userroles = _userroleRepository
                            .GetAll()
                            .Where(m => m.UserId == userid)
                            .ToList();
                    #endregion
                    #region[GetAllRole]
                    List<Role> roles = null;
                    int totalRoles = new int();

                    if (!string.IsNullOrEmpty(filter))
                    {
                        roles = _roleRepository
                            .FindBy(m => m.Name.ToLower()
                            .Contains(filter.ToLower().Trim()))
                            .Where(m => m.Delete == false && m.Active == true)
                            .OrderBy(m => m.Id)
                            .Skip(currentPage * currentPageSize)
                            .Take(currentPageSize)
                            .ToList();

                        totalRoles = _roleRepository
                            .FindBy(m => m.Name.ToLower()
                            .Contains(filter.ToLower().Trim()))
                            .Where(m => m.Delete == false && m.Active == true)
                            .Count();
                    }
                    else
                    {
                        roles = _roleRepository
                            .GetAll()
                            .Where(m => m.Delete == false && m.Active == true)
                            .OrderBy(m => m.Id)
                            .Skip(currentPage * currentPageSize)
                            .Take(currentPageSize)
                            .ToList();

                        totalRoles = _roleRepository.GetAll().Where(m => m.Delete == false && m.Active == true).Count();
                    }
                    IEnumerable<UserRoleViewModel> rolesVM = Mapper.Map<IEnumerable<Role>, IEnumerable<UserRoleViewModel>>(roles);
                    #endregion
                    rolesVM.UpdateUserRoleViewModel(userroles, userid);
                    PaginationUserRolesSet<UserRoleViewModel> pagedSet = new PaginationUserRolesSet<UserRoleViewModel>()
                    {
                        Page = currentPage,
                        TotalCount = totalRoles,
                        TotalPages = (int)Math.Ceiling((decimal)totalRoles / currentPageSize),
                        Items = rolesVM
                    };

                    response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                return response;
            });
        }

        [Authorize(Roles = "CreateUserRoles")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, UserRoleViewModel role)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var userroledb = _userroleRepository.GetAll().Where(x => x.RoleId == role.RoleID && x.UserId == role.UserID).ToList();
                    if (userroledb.Count > 0)
                    {
                        var userroledbupdate = _userroleRepository.GetSingle(userroledb[0].Id);
                        userroledbupdate.Active = role.Active;
                        _userroleRepository.Edit(userroledbupdate);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Active thành công");
                    }
                    else
                    {

                        UserRole newUserRole = new UserRole();
                        newUserRole.UpdateUserRole(role);
                        _userroleRepository.Add(newUserRole);
                        _unitOfWork.Commit();
                        response = request.CreateErrorResponse(HttpStatusCode.OK, "Active thành công");
                    }
                }
                return response;
            });
        }
    }
}