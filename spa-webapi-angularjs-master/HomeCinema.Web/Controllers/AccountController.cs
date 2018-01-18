using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Services;
using HomeCinema.Services.Utilities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HomeCinema.Data.Extensions;
using HomeCinema.Web.Infrastructure.Extensions;

namespace HomeCinema.Web.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiControllerBase
    {
        private readonly IMembershipService _membershipService;
        private readonly IEntityGuidRepository<User> _usersRepository;
        public AccountController(IMembershipService membershipService, IEntityGuidRepository<User> usersRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _membershipService = membershipService;
            _usersRepository = usersRepository;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    MembershipContext _userContext = _membershipService.ValidateUser(user.Username, user.Password);

                    if (_userContext.User != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }
                else
                    response = request.CreateResponse(HttpStatusCode.OK, new { success = false });

                return response;
            });
        }

        [Route("hello")]
        public HttpResponseMessage get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                 response = request.CreateResponse(HttpStatusCode.OK,"không ra nghỉ game");
              

                return response;
            });
        }
        [Authorize(Roles = "RegisterUser")]
        //[AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel user)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
                }
                else
                {
                    Entities.User _user = _membershipService.CreateUser(user.Username, user.Email, user.Password);

                    if (_user != null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }
                    else
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { success = false });
                    }
                }

                return response;
            });
        }
        //[Authorize(Roles = "CreateCalendar")]
        [Route("Getuserbyleader")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userName = HttpContext.Current.User.Identity.Name;

                List<User> users = new List<User>();

                if (HttpContext.Current.User.IsInRole("Getuserbyleader"))
                {
                    users = _usersRepository.GetAll().Where(

                       m => m.IsLocked == false &&

                       m.Username != userName &&

                       m.UserRoles.All(r => r.Role.Name != "Admin")

                       ).ToList();
                }
                IEnumerable<UserCalendarViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserCalendarViewModel>>(users);
                if (users.Count > 0)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, usersVM);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, usersVM);
                }
                return response;
            });
        }
        [Route("all")]
        public HttpResponseMessage Getall(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var userName = HttpContext.Current.User.Identity.Name;
                HttpResponseMessage response = null;
                var users = _usersRepository.GetAll().Where(

                    m => m.IsLocked == false &&

                    m.UserRoles.All(r => r.Role.Name != "Admin")

                    ).ToList();

                IEnumerable<UserViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

                response = request.CreateResponse<IEnumerable<UserViewModel>>(HttpStatusCode.OK, usersVM);

                return response;
            });
        }
        [Authorize(Roles = "GetUser")]
        [HttpGet]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Search(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<User> users = null;
                var userName = HttpContext.Current.User.Identity.Name;
                int totalUsers = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Trim().ToLower();
                    users = _usersRepository.FindBy(c =>
                         c.Username.ToLower().Contains(filter) ||
                         c.Email.ToLower().Contains(filter))
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalUsers = _usersRepository.FindBy(c => 
                    c.Username.ToLower().Contains(filter) ||
                            c.Email.ToLower().Contains(filter))
                            .Count();
                }
                else
                {
                    users = _usersRepository.GetAll()
                        .OrderBy(c => c.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                    .ToList();

                    totalUsers = _usersRepository.GetAll().Count();
                }

                IEnumerable<UserViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
                PaginationSet<UserViewModel> pagedSet = new PaginationSet<UserViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalUsers,
                    TotalPages = (int)Math.Ceiling((decimal)totalUsers / currentPageSize),
                    Items = usersVM
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route("Getusernamepermission")]
        public HttpResponseMessage Getuserpermission(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var userName = HttpContext.Current.User.Identity.Name;

                List<User> users = new List<User>();

                users = _usersRepository.GetAll().Where(

                   m => m.IsLocked == false &&

                   m.Username == userName &&

                   m.UserRoles.All(r => r.Active == true && r.Role.Active == true && r.Role.Delete == false)

                   ).ToList();
                IEnumerable<UserPermissionViewModel> usersVM = Mapper.Map<IEnumerable<User>, IEnumerable<UserPermissionViewModel>>(users);
                if (users.Count > 0)
                {
                    response = request.CreateResponse(HttpStatusCode.OK, usersVM);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, usersVM);
                }
                return response;
            });
        }

    }
}
