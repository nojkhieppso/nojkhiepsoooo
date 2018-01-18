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

    [Authorize]
    [RoutePrefix("api/school")]
    public class SchoolController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<School> _schoolRepository;

        public SchoolController(
            IEntityGuidRepository<School> schoolRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _schoolRepository = schoolRepository;
        }

        [Authorize(Roles ="Getschool")]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var classs = _schoolRepository.GetAll().OrderBy(m => m.Name).ToList();

                IEnumerable<SchoolViewModel> classsVM = Mapper.Map<IEnumerable<School>, IEnumerable<SchoolViewModel>>(classs);

                response = request.CreateResponse(HttpStatusCode.OK, classsVM);

                return response;
            });
        }
        [Authorize(Roles = "Getschool")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var school = _schoolRepository.GetSingle(id);

                SchoolViewModel classVM = Mapper.Map<School, SchoolViewModel>(school);

                response = request.CreateResponse(HttpStatusCode.OK, classVM);

                return response;
            });
        }
        [Authorize(Roles = "Getschool")]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<School> classs = null;
                int totalSchools = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    classs = _schoolRepository
                        .FindBy(m => m.Name.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m=>m.Delete==false &&m.Active==true)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalSchools = _schoolRepository
                        .FindBy(m => m.Name.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m => m.Delete == false && m.Active == true)
                        .Count();
                }
                else
                {
                    classs = _schoolRepository
                        .GetAll()
                        .Where(m => m.Delete == false && m.Active == true)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalSchools = _schoolRepository.GetAll().Where(m => m.Delete == false && m.Active == true).Count();
                }

                IEnumerable<SchoolViewModel> classsVM = Mapper.Map<IEnumerable<School>, IEnumerable<SchoolViewModel>>(classs);

                PaginationSet<SchoolViewModel> pagedSet = new PaginationSet<SchoolViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalSchools,
                    TotalPages = (int)Math.Ceiling((decimal)totalSchools / currentPageSize),
                    Items = classsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Authorize(Roles = "Createschool")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, SchoolViewModel school)
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
                    school.Id = Guid.NewGuid();
                    School newSchool = new School();
                    newSchool.UpdateSchool(school);
                    _schoolRepository.Add(newSchool);
                    _unitOfWork.Commit();
                    school = Mapper.Map<School, SchoolViewModel>(newSchool);
                    response = request.CreateResponse(HttpStatusCode.Created, school);
                }
                return response;
            });
        }
        [Authorize(Roles = "Editschool")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, SchoolViewModel school)
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
                    var classDb = _schoolRepository.GetSingle(school.Id);
                    if (classDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid class.");
                    else
                    {
                        classDb.UpdateSchool(school);
                        _schoolRepository.Edit(classDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, school);
                    }
                }

                return response;
            });
        }

        [Authorize(Roles = "DeleteSchool")]
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
                    var schoolDb = _schoolRepository.GetSingle(id);
                    if (schoolDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        schoolDb.Delete = true;
                        _schoolRepository.Edit(schoolDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }

                return response;
            });
        }
    }

}