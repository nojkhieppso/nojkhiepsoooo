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
    [RoutePrefix("api/lession")]
    public class LessionsController : ApiControllerBase
    {
        private readonly IEntityGuidRepository<Lession> _lessonsRepository;

        public LessionsController(
            IEntityGuidRepository<Lession> lessonsRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _lessonsRepository = lessonsRepository;
        }

        [Authorize(Roles ="Getlession")]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lessons = _lessonsRepository.GetAll().OrderBy(m => m.StartAt).ToList();

                IEnumerable<LessionViewModel> lessonsVM = Mapper.Map<IEnumerable<Lession>, IEnumerable<LessionViewModel>>(lessons);

                response = request.CreateResponse(HttpStatusCode.OK, lessonsVM);

                return response;
            });
        }
        [Authorize(Roles = "Getlession")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var lesson = _lessonsRepository.GetSingle(id);

                LessionaddViewModel lessonVM = Mapper.Map<Lession, LessionaddViewModel>(lesson);

                response = request.CreateResponse(HttpStatusCode.OK, lessonVM);

                return response;
            });
        }
        [Authorize(Roles = "Getlession")]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Lession> lessons = null;
                int totalLessions = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    lessons = _lessonsRepository
                        .FindBy(m => m.Description.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m=>m.Delete==false)
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalLessions = _lessonsRepository
                        .FindBy(m => m.Description.ToLower().ToString()
                        .Contains(filter.ToLower().Trim()))
                        .Where(m=>m.Delete==false)
                        .Count();
                }
                else
                {
                    lessons = _lessonsRepository
                        .GetAll()
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalLessions = _lessonsRepository.GetAll().Count();
                }

                IEnumerable<LessionViewModel> lessonsVM = Mapper.Map<IEnumerable<Lession>, IEnumerable<LessionViewModel>>(lessons);

                PaginationSet<LessionViewModel> pagedSet = new PaginationSet<LessionViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalLessions,
                    TotalPages = (int)Math.Ceiling((decimal)totalLessions / currentPageSize),
                    Items = lessonsVM
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Authorize(Roles = "Createlession")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, LessionaddViewModel lesson)
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
                    Lession newLession = new Lession();
                    newLession.UpdateLesson(lesson);
                    _lessonsRepository.Add(newLession);

                    _unitOfWork.Commit();

                        // Update view model
                        lesson = Mapper.Map<Lession, LessionaddViewModel>(newLession);
                    response = request.CreateResponse(HttpStatusCode.Created, lesson);
                }

                return response;
            });
        }
        [Authorize(Roles = "Editlession")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, LessionaddViewModel lesson)
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
                    var lessonDb = _lessonsRepository.GetSingle(lesson.Id);
                    if (lessonDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid lesson.");
                    else
                    {
                        lessonDb.UpdateLesson(lesson);
                        _lessonsRepository.Edit(lessonDb);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, lesson);
                    }
                }

                return response;
            });
        }

        [Authorize(Roles = "DeleteLession")]
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
                    var lessionDb = _lessonsRepository.GetSingle(id);
                    if (lessionDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        lessionDb.Delete = true;
                        _lessonsRepository.Edit(lessionDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }

                return response;
            });
        }
    }

}