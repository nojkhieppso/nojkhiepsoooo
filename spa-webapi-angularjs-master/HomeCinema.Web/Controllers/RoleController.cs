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
using System.Runtime.Caching;

namespace HomeCinema.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/role")]
    public class RolesController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<Role> _roleRepository;
        MemoryCache memCache = MemoryCache.Default;
        public RolesController(IEntityGuidRepository<Role> roleRepository,
             IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _roleRepository = roleRepository;
        }
        [Authorize(Roles = "GetRole")]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var role = _roleRepository.GetAll().Where(m => m.Delete == false && m.Active == true).ToList();
                IEnumerable<RoleViewModel> roleVM = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(role);
                response = request.CreateResponse<IEnumerable<RoleViewModel>>(HttpStatusCode.OK, roleVM);
                return response;
            });
        }

        [Authorize(Roles = "GetRole")]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Role> roles = null;
                int totalRoles = new int();
                if (!string.IsNullOrEmpty(filter))
                {
                    roles = _roleRepository
                        .FindBy(m => m.Name.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m => m.Delete == false)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalRoles = _roleRepository
                        .FindBy(m => m.Name.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m => m.Delete == false)
                        .Count();
                }
                else
                {
                    roles = _roleRepository
                        .GetAll()
                        .Where(m => m.Delete == false)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalRoles = _roleRepository.GetAll().Where(m => m.Delete == false).Count();
                }

                IEnumerable<RoleViewModel> rolesVM = Mapper.Map<IEnumerable<Role>, IEnumerable<RoleViewModel>>(roles);

                PaginationSet<RoleViewModel> pagedSet = new PaginationSet<RoleViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalRoles,
                    TotalPages = (int)Math.Ceiling((decimal)totalRoles / currentPageSize),
                    Items = rolesVM
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }


        [Authorize(Roles = "GetRole")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var role = _roleRepository.GetSingle(id);

                RoleViewModel roleVM = Mapper.Map<Role, RoleViewModel>(role);

                response = request.CreateResponse(HttpStatusCode.OK, roleVM);

                return response;
            });
        }

        [Authorize(Roles = "DeleteRole")]
        [HttpPost]
        [Route("delete/{id:Guid}")]
        public HttpResponseMessage Delete(HttpRequestMessage request, Guid id)
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
                    var roleDb = _roleRepository.GetSingle(id);
                    if (roleDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        roleDb.Delete = true;
                        _roleRepository.Edit(roleDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }

                return response;
            });
        }

        [Authorize(Users = "nojkhiepso",Roles ="ActiveRole")]
        [HttpPost]
        [Route("active/{id:Guid}/{active}")]
        public HttpResponseMessage Active(HttpRequestMessage request, Guid id, string active)
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
                    var roleDb = _roleRepository.GetSingle(id);
                    if (roleDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        roleDb.Active = Common.truefalse(active);
                        _roleRepository.Edit(roleDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Active thành công");
                    }
                }

                return response;
            });
        }
        [Authorize(Users = "nojkhiepso")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, RoleViewModel role)
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
                    role.ID = Guid.NewGuid();
                    Role newRole = new Role();
                    newRole.UpdateRole(role);
                    _roleRepository.Add(newRole);
                    _unitOfWork.Commit();
                    // Update view model
                    role = Mapper.Map<Role, RoleViewModel>(newRole);
                    response = request.CreateResponse(HttpStatusCode.Created, role);
                }
                return response;
            });
        }

        [Authorize(Roles = "EditRole")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, RoleViewModel role)
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
                    var roleDb = _roleRepository.GetSingle(role.ID);
                    if (roleDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Có tìm thấy đâu mà");
                    else
                    {
                        roleDb.UpdateRole(role);
                        _roleRepository.Edit(roleDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, role);
                    }
                }
                return response;
            });
        }
    }
}
