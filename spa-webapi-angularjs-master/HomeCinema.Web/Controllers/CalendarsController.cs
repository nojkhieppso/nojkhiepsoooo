using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HomeCinema.Web.Infrastructure.Extensions;
using System.Web;
using System.Security.Claims;
using System.Runtime.Caching;

namespace HomeCinema.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/calendars")]
    public class CalendarsController : ApiControllerBase
    {
        
        private readonly IEntityGuidRepository<Entities.Calendar> _calendarsRepository;
        private readonly IEntityGuidRepository<User> _usersRepository;
        private readonly IEntityGuidRepository<CalenderLession> _calenderlessonRepository;
        private readonly IEntityGuidRepository<Lession> _lessonRepository;
        private readonly IEntityGuidRepository<Classroom> _classroomRepository;
        private readonly IEntityGuidRepository<School> _schoolRepository;
        public CalendarsController(
            IEntityGuidRepository<School> schoolRepository,
            IEntityGuidRepository<Entities.Calendar> calendarsRepository,
            IEntityGuidRepository<User> usersRepository,
            IEntityGuidRepository<CalenderLession> calenderlessonRepository,
            IEntityGuidRepository<Lession> lessonRepository,
            IEntityGuidRepository<Classroom> classroomRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork
            )
            : base(_errorsRepository, _unitOfWork)
        {
            _calendarsRepository = calendarsRepository;
            _usersRepository = usersRepository;
            _lessonRepository = lessonRepository;
            _calenderlessonRepository = calenderlessonRepository;
            _classroomRepository = classroomRepository;
            _schoolRepository = schoolRepository;
        }

        //[Authorize(Roles = "GetCalendar")]
        [AllowAnonymous]
        [Route("latest")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                string username = identity.Claims.First().Value;
                var userName = HttpContext.Current.User.Identity.Name;
                string id = User.Identity.Name;
                string name = RequestContext.Principal.Identity.Name;

                List<Entities.Calendar> calendars = new List<Entities.Calendar>();

                //if (HttpContext.Current.User.IsInRole("CreateCalendar") || HttpContext.Current.User.IsInRole("EditCalendar"))
                //{
                calendars = _calendarsRepository.GetAll().ToList();
                //}
                //else
                //{
                //    calendars = _calendarsRepository.GetAll().Where(c => c.Description == userName).ToList();
                //}

                IEnumerable<CalendarViewModel> calendarsVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewModel>>(calendars);

                response = request.CreateResponse(HttpStatusCode.OK, calendarsVM);

                return response;
            });
        }

        [Authorize(Roles = "GetCalendar")]
        [Route("details/{id:Guid}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var calendar = _calendarsRepository.GetSingle(id);

                CalendarViewModel calendarVM = Mapper.Map<Entities.Calendar, CalendarViewModel>(calendar);

                response = request.CreateResponse(HttpStatusCode.OK, calendarVM);

                return response;
            });
        }

        [Authorize(Roles = "GetCalendar")]
        [Route("search/{page:int=0}/{pageSize=3}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Entities.Calendar> calendars = null;
                int totalCalendars = new int();

                if (!string.IsNullOrEmpty(filter))
                {
                    calendars = _calendarsRepository
                        .FindBy(m => m.Description.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalCalendars = _calendarsRepository
                        .FindBy(m => m.Description.ToLower()
                        .Contains(filter.ToLower().Trim()))
                        .Count();
                }
                else
                {
                    calendars = _calendarsRepository
                        .GetAll()
                        .OrderBy(m => m.Id)
                        .Skip(currentPage * currentPageSize)
                        .Take(currentPageSize)
                        .ToList();

                    totalCalendars = _calendarsRepository.GetAll().Count();
                }

                IEnumerable<CalendarViewModel> calendarsVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewModel>>(calendars);

                PaginationSet<CalendarViewModel> pagedSet = new PaginationSet<CalendarViewModel>()
                {
                    Page = currentPage,
                    TotalCount = totalCalendars,
                    TotalPages = (int)Math.Ceiling((decimal)totalCalendars / currentPageSize),
                    Items = calendarsVM
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }


        [Authorize(Roles = "GetCalendar")]
        [Route("searchr/{page:int=0}/{pageSize=3}/{userid:int=0}/{StartAt:DateTime?}/{EndAt:DateTime?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, Guid? userid, int? page, int? pageSize, DateTime? StartAt, DateTime? EndAt)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            DateTime? Start = DateTime.Parse("01/01/1900"); ;
            DateTime? End = DateTime.Now;
            DateTime Startm = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            DateTime Endm = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25);
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<Entities.Calendar> calendars = null;
                List<Entities.Calendar> calendartotal = null;
                List<Entities.Calendar> calendartotalmonth = null;
                double Totalmonth = 0;
                int totalCalendars = new int();
                int insert = (int?)null ?? Data.Common.common.searchrBool(StartAt.ToString(), EndAt.ToString());
                if (userid != null)
                {
                    var username = _usersRepository.GetSingle(userid.Value);
                    if (insert > 0)
                    {
                        switch (insert)
                        {
                            case 1:
                                Start = DateTime.Parse("01/01/1900");
                                End = EndAt;
                                break;
                            case 2:
                                Start = StartAt;
                                break;
                            case 3:
                                Start = StartAt;
                                End = EndAt;
                                break;
                        }
                        calendars = _calendarsRepository
                         .GetAll().Where(m => m.Description == username.Username && m.StartAt >= Start && m.EndAt <= End)
                          .OrderBy(m => m.StartAt)
                          .Skip(currentPage * currentPageSize)
                          .Take(currentPageSize)
                          .ToList();
                        calendartotal = _calendarsRepository
                         .GetAll().Where(m => m.Description == username.Username && m.StartAt >= Start && m.EndAt <= End)
                          .OrderBy(m => m.StartAt)
                          .ToList();
                        totalCalendars = _calendarsRepository
                            .GetAll()
                            .Where(m => m.Description == username.Username && m.StartAt >= Start && m.EndAt <= End)
                            .Count();
                    }
                    else
                    {
                        calendartotal = _calendarsRepository
                                    .GetAll().Where(m => m.Description == username.Username)
                                    .OrderBy(m => m.Id)

                                    .ToList();
                        calendars = _calendarsRepository
                                    .GetAll().Where(m => m.Description == username.Username)
                                    .OrderBy(m => m.Id)
                                    .Skip(currentPage * currentPageSize)
                                    .Take(currentPageSize)
                                    .ToList();
                        totalCalendars = _calendarsRepository.GetAll().Where(m => m.Description == username.Username).Count();
                    }

                    //tinh tien thang


                    calendartotalmonth = _calendarsRepository
                         .GetAll().Where(m => m.Description == username.Username && m.StartAt >= Start && m.EndAt <= End)
                          .OrderBy(m => m.StartAt)
                          .Skip(currentPage * currentPageSize)
                          .Take(currentPageSize)
                          .ToList();

                    IEnumerable<CalendarViewDetailModel> calendartotalmonthVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewDetailModel>>(calendartotalmonth);
                    List<CalendarViewDetailModel> calendartotalmonths = calendartotalmonthVM.Updatecalendartotal();
                    Totalmonth = (from od in calendartotalmonths
                                  select od.TienGiang).Sum();

                }

                IEnumerable<CalendarViewDetailModel> calendarsVMTotal = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewDetailModel>>(calendartotal);

                List<CalendarViewDetailModel> calendaruppdate = calendarsVMTotal.Updatecalendartotal();

                double Total = (from od in calendaruppdate
                                select od.TienGiang).Sum();

                IEnumerable<CalendarViewDetailModel> calendarsVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewDetailModel>>(calendars);

                IEnumerable<CalendarViewDetailModel> calendarsupdate = calendarsVM.Updatecalendartotal();
                //CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
                PaginationSetExtention<CalendarViewDetailModel> pagedSet = new PaginationSetExtention<CalendarViewDetailModel>()
                {

                    Page = currentPage,
                    TotalCount = totalCalendars,
                    TotalPages = (int)Math.Ceiling((decimal)totalCalendars / currentPageSize),
                    Items = calendarsupdate,
                    Totalcurrency = Total.ToString() + " vnđ (Từ " + DateTime.Parse(Start.ToString()).ToString("dd/MM/yyyy") + " đến " + DateTime.Parse(End.ToString()).ToString("dd/MM/yyyy") + ")",
                    Totalmonth = Totalmonth.ToString() + " vnđ (Từ " + Startm.ToString("dd/MM/yyyy") + " đến " + Endm.ToString("dd/MM/yyyy") + ")"
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        //[Authorize(Roles = "GetCalendar")]
        [AllowAnonymous]
        [Route("searchr/{start:DateTime?}/{end:DateTime?}/{_=1489729323792}")]
        public HttpResponseMessage Get(HttpRequestMessage request, DateTime? start, DateTime? end)
        {

            DateTime? Start = null;
            DateTime? End = DateTime.Now;
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<Entities.Calendar> calendars = null;
                var userName = HttpContext.Current.User.Identity.Name;
                int insert = (int?)null ?? Data.Common.common.searchrBool(start.ToString(), end.ToString());
                int totalCalendars = new int();
                if (insert > 0)
                {
                    switch (insert)
                    {
                        case 1:
                            Start = DateTime.Parse("01/01/1900");
                            End = end;
                            break;
                        case 2:
                            Start = start;
                            break;
                        case 3:
                            Start = start;
                            End = end;
                            break;
                    }
                }
                if (HttpContext.Current.User.IsInRole("CreateCalendar") || HttpContext.Current.User.IsInRole("EditCalendar"))
                {


                    if (insert > 0)
                    {
                        calendars = _calendarsRepository
                         .GetAll().Where(m => m.StartAt >= Start && m.EndAt <= End)
                          .OrderBy(m => m.StartAt)
                          .ToList();

                        totalCalendars = _calendarsRepository
                            .GetAll()
                            .Where(m => m.StartAt >= Start && m.EndAt <= End)
                            .Count();
                    }
                    else
                    {
                        calendars = _calendarsRepository
                                    .GetAll()
                                    .OrderBy(m => m.Id)
                                    .ToList();

                        totalCalendars = _calendarsRepository.GetAll().Count();
                    }
                }
                else
                {

                    if (insert > 0)
                    {
                        calendars = _calendarsRepository.GetAll().Where(c => c.Description == userName && c.StartAt >= Start && c.EndAt <= End).ToList();
                    }
                    else
                    {
                        calendars = _calendarsRepository
                                    .GetAll()
                                    .OrderBy(m => m.Id)
                                    .ToList();

                        totalCalendars = _calendarsRepository.GetAll().Count();
                    }

                }
                IEnumerable<CalendarViewModel> calendarsVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarViewModel>>(calendars);



                response = request.CreateResponse(HttpStatusCode.OK, calendarsVM);
                return response;
            });
        }

        [AllowAnonymous]
        [Route("allgroup")]
        public HttpResponseMessage Getallgroups(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                HttpResponseMessage response = null;
                List<Entities.Calendar> calendars = null;
                if (HttpContext.Current.User.IsInRole("CreateCalendar") || HttpContext.Current.User.IsInRole("EditCalendar"))
                {
                    calendars = _calendarsRepository
                                .GetAll()
                                .OrderBy(m => m.Id)
                                .ToList();
                }
                else
                {
                    calendars = _calendarsRepository
                                    .GetAll()
                                    .OrderBy(m => m.Id)
                                    .ToList();
                }
                IEnumerable<CalendarGroupsViewModel> calendarsVM = Mapper.Map<IEnumerable<Entities.Calendar>, IEnumerable<CalendarGroupsViewModel>>(calendars.Distinct());
                var calendarsdistinc = calendarsVM.Select(o => new { o.Name, o.title }).Distinct();
                var calendaruppdate = new List<CalendarGroupsViewModel>();
                foreach (var CalendarGroups in calendarsdistinc)
                {
                    var calendaritem = new CalendarGroupsViewModel()
                    {
                        Id = HomeCinema.Data.Common.common.Generate(CalendarGroups.title + "(" + CalendarGroups.Name + ")"),
                        Name = CalendarGroups.Name,
                        title = CalendarGroups.title + "(" + CalendarGroups.Name + ")"
                    };
                    calendaruppdate.Add(calendaritem);
                }
                response = request.CreateResponse(HttpStatusCode.OK, calendaruppdate);
                return response;
            });
        }

        
        [Authorize(Roles = "CreateCalendar")]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, CalendarLessionViewModel calendar)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Guid userid = Guid.Parse(calendar.UserID);
                Guid lessionid = calendar.lessionID.Value != null ? calendar.lessionID.Value : Guid.Empty;
                Guid schoolid = calendar.SchoolId.Value;
                if (userid != null && lessionid != null)
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var User = _usersRepository.GetAll().Where(m => m.Id == userid).ToList();
                        var lession = _lessonRepository.GetSingle(lessionid);
                        var school = _schoolRepository.GetSingle(schoolid);
                        if (User.Count > 0 && lession.Id != null && school != null)
                        {
                            calendar.UserID = User[0].Id.ToString();
                            calendar.start = lession.StartAt.ToString();
                            calendar.end = lession.EndAt.ToString();
                            List<Profiler> profiler = User.SelectMany(p => p.Profiler).ToList();
                            calendar.backgroundColor = (profiler.Count > 0) ? profiler[0].Color : "#125d14";
                            calendar.borderColor = (profiler.Count > 0) ? profiler[0].Color : "#125d14";
                            calendar.Description = User[0].Username;
                            Entities.Calendar newCalendar = new Entities.Calendar();
                            newCalendar.UpdateCalendarView(calendar);
                            _calendarsRepository.Add(newCalendar);
                            _unitOfWork.Commit();
                            // Update view model
                            calendar = Mapper.Map<Entities.Calendar, CalendarLessionViewModel>(newCalendar);
                            CalenderLession calenderlession = new CalenderLession();
                            calenderlession.SchoolId = school.Id;
                            calenderlession.LessionId = lession.Id;
                            calenderlession.CalendarId = calendar.ID;
                            _calenderlessonRepository.Add(calenderlession);
                            _unitOfWork.Commit();
                            response = request.CreateResponse(HttpStatusCode.Created, calendar);
                        }

                    }
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Error");
                }
                return response;
            });
        }

        [Authorize(Roles = "CreateCalendar")]
        [HttpPost]
        [Route("Adddrag")]
        public HttpResponseMessage Adddrag(HttpRequestMessage request, CalendarLessionViewModel calendar)
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
                    var User = _usersRepository.GetAll().Where(m => m.Username == calendar.title).ToList();

                    if (User.Count > 0)
                    {
                        calendar.UserID = User[0].Id.ToString();
                        List<Profiler> profiler = User.SelectMany(p => p.Profiler).ToList();
                        calendar.backgroundColor = (profiler.Count > 0) ? profiler[0].Color : "#125d14";
                        calendar.borderColor = (profiler.Count > 0) ? profiler[0].Color : "#125d14";
                        Entities.Calendar newCalendar = new Entities.Calendar();
                        newCalendar.UpdateCalendar(calendar);
                        _calendarsRepository.Add(newCalendar);
                        _unitOfWork.Commit();
                        // Update view model
                        calendar = Mapper.Map<Entities.Calendar, CalendarLessionViewModel>(newCalendar);

                        CalenderLession calenderlession = new CalenderLession();
                        calenderlession.CalendarId = calendar.ID;
                        _calenderlessonRepository.Add(calenderlession);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.Created, calendar);
                    }

                }

                return response;
            });
        }



        [Authorize(Roles = "EditCalendar")]
        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, CalendarLessionViewModel calendar)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                Guid? lessionID = Guid.Empty;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var calendarlession = _calenderlessonRepository.GetAll().Where(x => x.CalendarId == calendar.CalendarId).ToList();

                    if (calendarlession == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid calendar.");
                    else
                    {
                        
                        var calendarlessionDb = _calenderlessonRepository.GetSingle(calendarlession[0].Id);
                        if (calendar.lessionID != null && calendar.SchoolId!=null)
                        {
                            lessionID = calendar.lessionID;

                            calendarlessionDb.UpdateCalenderLession(calendar);

                            _calenderlessonRepository.Edit(calendarlessionDb);

                            
                        }
                        else
                        {
                            lessionID = calendarlessionDb.LessionId;
                        }

                        var lessiondb = _lessonRepository.GetSingle(lessionID.Value);

                        var calendardb = _calendarsRepository.GetSingle(Guid.Parse(calendarlession[0].CalendarId.ToString()));

                        calendar.start = lessiondb.StartAt.ToString();

                        calendar.end = lessiondb.EndAt.ToString();

                        calendar.Description = calendarlession[0].Calendar.Description;

                        calendar.borderColor = calendardb.borderColor;

                        calendar.backgroundColor = calendardb.backgroundColor;

                        calendardb.UpdateCalendarView(calendar);

                        _calendarsRepository.Edit(calendardb);

                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK, calendarlessionDb);
                    }
                }

                return response;
            });
        }

        [Authorize(Roles = "EditCalendar")]
        [HttpPost]
        [Route("updatedrag")]
        public HttpResponseMessage Updatedrag(HttpRequestMessage request, CalendarLessionViewModel calendar)
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

                    var calendardb = _calendarsRepository.GetSingle(calendar.CalendarId);
                    if (calendardb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid calendar.");
                    else
                    {
                        calendar.end = calendardb.EndAt.ToString("HH:mm");
                        calendar.start = calendardb.StartAt.ToString("HH:mm");
                        calendardb.UpdateCalendardragView(calendar);

                        _calendarsRepository.Edit(calendardb);

                        _unitOfWork.Commit();

                        response = request.CreateResponse(HttpStatusCode.OK, calendardb);
                    }
                }

                return response;
            });
        }





        [Authorize(Roles = "DeleteCalendar")]
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
                    var calendarDb = _calendarsRepository.GetSingle(id);
                    if (calendarDb == null)
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy bản ghi");
                    else
                    {
                        _calendarsRepository.Delete(calendarDb);
                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK, "Xóa bản ghi thành công");
                    }
                }
                return response;
            });
        }
    }
}