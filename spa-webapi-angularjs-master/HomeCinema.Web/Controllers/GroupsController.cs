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
    [RoutePrefix("api/group")]
    public class GroupsController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<Group> _groupsRepository;
        private readonly IEntityGuidRepository<User> _userRepository;
        public GroupsController(IEntityGuidRepository<Group> groupsRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _groupsRepository = groupsRepository;
        }

        [Authorize(Roles = "GetGroup")]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var groups = _groupsRepository.GetAll().ToList();

                IEnumerable<GroupViewModel> groupsVM = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(groups);

                response = request.CreateResponse(HttpStatusCode.OK, groups);

                return response;
            });
        }
        [Authorize(Roles = "GetGroup")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var group = _groupsRepository.GetSingle(id);
                GroupViewModel groupVM = Mapper.Map<Group, GroupViewModel>(group);
                response = request.CreateResponse(HttpStatusCode.OK, groupVM);
                return response;
            });
        }

        [Authorize(Roles = "GetGroup")]
        [Route("{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Group> groups = null;
                int totalGroups = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    groups = _groupsRepository
                        .FindBy(m => m.Name.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalGroups = _groupsRepository
                        .FindBy(m => m.Name.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .Count();
                }
                else
                {
                    groups = _groupsRepository
                        .GetAll()
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalGroups = _groupsRepository.GetAll().Count();
                }

                IEnumerable<GroupViewModel> groupsVM = Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(groups);

                PaginationSet<GroupViewModel> pagedSet = new PaginationSet<GroupViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalGroups,
                    TotalPages = (int)Math.Ceiling((decimal)totalGroups / currentPageSize),
                    Items = groupsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Authorize(Roles = "CreateGroup")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, GroupViewModel group)
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
                    
                    Group newGroup = new Group();
                    newGroup.UpdateGroup(group);
                    _groupsRepository.Add(newGroup);

                    _unitOfWork.Commit();

                    // Update view model
                    group = Mapper.Map<Group, GroupViewModel>(newGroup);
                    response = request.CreateResponse(HttpStatusCode.Created, group);
                }

                return response;
            });
        }

        [Authorize(Roles = "EditGroup")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, GroupViewModel group)
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
                    var groupDb = _groupsRepository.GetSingle(group.ID);
                    if (groupDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid group.");
                    else
                    {
                        groupDb.UpdateGroup(group);
                        _groupsRepository.Edit(groupDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, group);
                    }
                }

                return response;
            });
        }

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
                    var groupDb = _groupsRepository.GetSingle(id);
                    if (groupDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        _groupsRepository.Delete(groupDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }

                return response;
            });
        }
    }
}