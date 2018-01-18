using HomeCinema.Data.Common;
using HomeCinema.Entities;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Extensions
{
    public static class EntitiesExtensions
    {
        public static void UpdateMovie(this Movie movie, MovieViewModel movieVm)
        {
            movie.Title = movieVm.Title;
            movie.Description = movieVm.Description;
            movie.GenreId = movieVm.GenreId;
            movie.Director = movieVm.Director;
            movie.Writer = movieVm.Writer;
            movie.Producer = movieVm.Producer;
            movie.Rating = movieVm.Rating;
            movie.TrailerURI = movieVm.TrailerURI;
            movie.ReleaseDate = movieVm.ReleaseDate;
        }
        //public static IEnumerable<UserPermissionViewModel> updatepermision(this IEnumerable<UserPermissionViewModel> source)
        //{
        //    var UserRoles = new ICollection<UserRole>();
        //    UserRoles = source.Select(x => x.UserRoles);
        //    if (source.Select(x => x.UserRoles).ToList().Count > 0)
        //    {
        //        foreach()
        //    }
        //    return permision;
        //}
        public static void UpdateCustomer(this Profiler customer, CustomerViewModel customerVm)
        {
            customer.FirstName = customerVm.FirstName;
            customer.LastName = customerVm.LastName;
            customer.IdentityCard = customerVm.IdentityCard;
            customer.Mobile = customerVm.Mobile;
            customer.DateOfBirth = customerVm.DateOfBirth;
            customer.Email = customerVm.Email;

            customer.RegistrationDate = (customer.RegistrationDate == DateTime.MinValue ? DateTime.Now : customerVm.RegistrationDate);
        }
        public static void UpdateImage(this Image image, ImageViewModel imageVm)
        {
            image.CreateDate = imageVm.CreateDate;
            image.FileLength = imageVm.FileLength;
            image.FileName = imageVm.FileName;
            image.LocalFilePath = imageVm.LocalFilePath;

        }

        public static void UpdateCalendar(this Entities.Calendar calendar, CalendarLessionViewModel calendarVm)
        {
            calendar.Id = Guid.NewGuid();
            calendar.Description = calendarVm.Description;
            calendar.EndAt = DateTime.Parse(calendarVm.end, null, System.Globalization.DateTimeStyles.RoundtripKind);
            calendar.IsFullDay = calendarVm.allDay;
            calendar.StartAt = DateTime.Parse(calendarVm.start, null, System.Globalization.DateTimeStyles.RoundtripKind);
            calendar.backgroundColor = calendarVm.backgroundColor;
            calendar.borderColor = calendarVm.borderColor;

        }

        public static void UpdateGroupID(IEnumerable<CalendarGroupsViewModel> calendarsdistinc)
        {
            var calendaruppdate = new List<CalendarGroupsViewModel>();
            foreach (var CalendarGroups in calendarsdistinc)
            {

                var calendaritem = new CalendarGroupsViewModel()
                {
                    Id = HomeCinema.Data.Common.common.Generate(CalendarGroups.Name + "(" + CalendarGroups.title + ")"),
                    Name = CalendarGroups.Name,
                    title = CalendarGroups.title
                };
                calendaruppdate.Add(calendaritem);
            }
        }


        //public static void UpdateGroupID(IEnumerable<CalendarGroupsViewModel> calendarsdistinc)
        //{
        //    var calendaruppdate = new List<CalendarGroupsViewModel>();
        //    foreach (var CalendarGroups in calendarsdistinc)
        //    {

        //        var calendaritem = new CalendarGroupsViewModel()
        //        {
        //            Id = HomeCinema.Data.Common.common.Generate(CalendarGroups.Name + "(" + CalendarGroups.title + ")"),
        //            Name=CalendarGroups.Name,
        //            title=CalendarGroups.title
        //        };
        //        calendaruppdate.Add(calendaritem);
        //    }
        //}

        //public static void UpdateGroupID(IEnumerable<CalendarGroupsViewModel> calendarsdistinc, CalendarGroupsViewModel calendargroupsVm)
        //{
        //    foreach (var CalendarGroups in calendarsdistinc)
        //    {
        //        CalendarGroups.Id = HomeCinema.Data.Common.common.Generate(CalendarGroups.Name + "("+ CalendarGroups.title + ")");
        //    }
        //}
        public static void UpdateCalendarView(this Entities.Calendar calendar, CalendarLessionViewModel calendarVm)
        {

            calendar.Description = calendarVm.Description;
            calendar.EndAt = DateTime.Parse(calendarVm.today.Resetdate(), null, System.Globalization.DateTimeStyles.RoundtripKind).AddHours(calendarVm.end.Hours24()).AddMinutes(calendarVm.end.Minutes24());
            calendar.IsFullDay = calendarVm.allDay;
            calendar.StartAt = DateTime.Parse(calendarVm.today.Resetdate(), null, System.Globalization.DateTimeStyles.RoundtripKind).AddHours(calendarVm.start.Hours24()).AddMinutes(calendarVm.start.Minutes24());
            calendar.backgroundColor = calendarVm.backgroundColor;
            calendar.borderColor = calendarVm.borderColor;
            calendar.Soluongchau = calendarVm.soluongchau;
        }

        public static void UpdateCalendardragView(this Entities.Calendar calendar, CalendarLessionViewModel calendarVm)
        {
            calendar.EndAt = DateTime.Parse(calendarVm.today.Resetdate(), null, System.Globalization.DateTimeStyles.RoundtripKind).AddHours(calendarVm.end.Hours24()).AddMinutes(calendarVm.end.Minutes24());
            calendar.StartAt = DateTime.Parse(calendarVm.today.Resetdate(), null, System.Globalization.DateTimeStyles.RoundtripKind).AddHours(calendarVm.start.Hours24()).AddMinutes(calendarVm.start.Minutes24());
        }


        public static void UpdateGroup(this Group group, GroupViewModel groupVm)
        {
            group.Name = groupVm.Name;
        }

        public static void UpdateLesson(this Lession lession, LessionaddViewModel lessionVm)
        {
            lession.Description = lessionVm.Description;
            lession.EndAt = lessionVm.EndAt;
            lession.Money = lessionVm.Money;
            lession.StartAt = lessionVm.StartAt;
            lession.Active = true;
            lession.Delete = false;
        }

        public static void UpdateClassroom(this Classroom classroom, ClassroomViewModel classroomVm)
        {
            classroom.Name = classroomVm.Name;
            classroom.Totalstudent = classroomVm.Totalstudent;
            classroom.Descriptions = classroomVm.Descriptions;
            classroom.Active = true;
            classroom.Delete = false;
        }

        public static List<CalendarViewDetailModel> Updatecalendartotal(this IEnumerable<CalendarViewDetailModel> calendarsVMTotal)
        {
            var calendaruppdate = new List<CalendarViewDetailModel>();
            foreach (var customer in calendarsVMTotal)
            {
                var lessioncalendar = customer.CalenderLession;
                double lession = lessioncalendar[0].Lession.Money.Value;
                int sl = (customer.soluongchau > 0) ? customer.soluongchau : 0;
                var calendaritem = new CalendarViewDetailModel()
                {
                    allDay = customer.allDay,
                    DayOfWeek = customer.DayOfWeek,
                    Description = customer.Description,
                    end = customer.end,
                    ID = customer.ID,
                    start = customer.start,
                    TienGiang = (lession * sl),
                    Time = customer.Time,
                    title = customer.title,
                    UserID = customer.UserID
                };
                calendaruppdate.Add(calendaritem);
            }
            return calendaruppdate;
        }

        public static void UpdateUserRoleViewModel(this IEnumerable<UserRoleViewModel> roleviewmodel, List<UserRole> lstrole, Guid userid)
        {
            foreach (var role in roleviewmodel)
            {
                role.Active = (lstrole.Where(x => x.RoleId == role.ID).ToList().Count > 0) ? lstrole.Where(x => x.RoleId == role.ID).ToList()[0].Active : false;
                role.UserID = userid;
            }
        }



        public static void UpdateRole(this Role role, RoleViewModel roleVm)
        {
            role.Id = roleVm.ID;
            role.Name = roleVm.Name;
            role.Description = roleVm.Description;
        }

        public static void UpdateUserRole(this UserRole userrole, UserRoleViewModel userroleVm)
        {
            userrole.RoleId = userroleVm.RoleID;
            userrole.UserId = userroleVm.UserID;
            userrole.Active = userroleVm.Active;
        }

        public static void UpdateSys_Unit(this Entities.Sys_Unit sys_unit, Sys_UnitViewModel sys_unitVm)
        {
            sys_unit.Note = sys_unitVm.Note;
            sys_unit.Website = sys_unitVm.Website;
            sys_unit.CreatedDate = DateTime.Now;
            sys_unit.ModifiedDate = DateTime.Now;
        }
        public static void UpdateKey_Sys_Unit(this Entities.Key_Sys_Unit key_sys_unit, Key_Sys_UnitViewModel key_sys_unitVm)
        {

            key_sys_unit.Encodedomain = key_sys_unitVm.Encodedomain;
            key_sys_unit.Key = key_sys_unitVm.Key;
            key_sys_unit.UnitID = key_sys_unitVm.UnitID;
        }
        public static void UpdateCalenderLession(this CalenderLession calendarlession, CalendarLessionViewModel calendarlessionVm)
        {
            calendarlession.SchoolId = calendarlessionVm.SchoolId;
            calendarlession.LessionId = calendarlessionVm.lessionID;
            calendarlession.CalendarId = calendarlessionVm.CalendarId;
        }

        public static void UpdateSchool(this School school, SchoolViewModel schoolVm)
        {
            school.Id = schoolVm.Id;
            school.Active = true;
            school.Delete = false;
            school.Description = schoolVm.Description;
            school.Name = schoolVm.Name;
        }

    }
}