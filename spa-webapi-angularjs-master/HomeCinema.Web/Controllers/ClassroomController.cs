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
    [RoutePrefix("api/classroom")]
    public class ClassroomController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<Classroom> _classsRepository;

        public ClassroomController(
            IEntityGuidRepository<Classroom> classsRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _classsRepository = classsRepository;
        }

        [Authorize(Roles ="Getclassroom")]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var classs = _classsRepository.GetAll().OrderBy(m => m.Name).ToList();

                IEnumerable<ClassroomViewModel> classsVM = Mapper.Map<IEnumerable<Classroom>, IEnumerable<ClassroomViewModel>>(classs);

                response = request.CreateResponse(HttpStatusCode.OK, classsVM);

                return response;
            });
        }
        [Authorize(Roles = "Getclassroom")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var classroom = _classsRepository.GetSingle(id);

                ClassroomViewModel classVM = Mapper.Map<Classroom, ClassroomViewModel>(classroom);

                response = request.CreateResponse(HttpStatusCode.OK, classVM);

                return response;
            });
        }
        [Authorize(Roles = "Getclassroom")]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Classroom> classs = null;
                int totalClassrooms = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    classs = _classsRepository
                        .FindBy(m => m.Name.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m=>m.Delete==false &&m.Active==true)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalClassrooms = _classsRepository
                        .FindBy(m => m.Name.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m => m.Delete == false && m.Active == true)
                        .Count();
                }
                else
                {
                    classs = _classsRepository
                        .GetAll()
                        .Where(m => m.Delete == false && m.Active == true)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalClassrooms = _classsRepository.GetAll().Where(m => m.Delete == false && m.Active == true).Count();
                }

                IEnumerable<ClassroomViewModel> classsVM = Mapper.Map<IEnumerable<Classroom>, IEnumerable<ClassroomViewModel>>(classs);

                PaginationSet<ClassroomViewModel> pagedSet = new PaginationSet<ClassroomViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalClassrooms,
                    TotalPages = (int)Math.Ceiling((decimal)totalClassrooms / currentPageSize),
                    Items = classsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Authorize(Roles = "Createclassroom")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ClassroomViewModel classroom)
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
                    Classroom newClassroom = new Classroom();
                    newClassroom.UpdateClassroom(classroom);
                    _classsRepository.Add(newClassroom);

                    _unitOfWork.Commit();

                    // Update view model
                    classroom = Mapper.Map<Classroom, ClassroomViewModel>(newClassroom);
                    response = request.CreateResponse(HttpStatusCode.Created, classroom);
                }

                return response;
            });
        }
        [Authorize(Roles = "Editclassroom")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ClassroomViewModel classroom)
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
                    var classDb = _classsRepository.GetSingle(classroom.Id);
                    if (classDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid class.");
                    else
                    {
                        classDb.UpdateClassroom(classroom);
                        _classsRepository.Edit(classDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, classroom);
                    }
                }

                return response;
            });
        }

        [Authorize(Roles = "DeleteClassroom")]
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
                    var classroomDb = _classsRepository.GetSingle(id);
                    if (classroomDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        classroomDb.Delete = true;
                        _classsRepository.Edit(classroomDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }

                return response;
            });
        }
    }

}