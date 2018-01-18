using AutoMapper;
using HomeCinema.Entities;
using HomeCinema.Web.Models;
using HomeCinema.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {

            Mapper.CreateMap<Movie, MovieViewModel>()
            .ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name))
            .ForMember(vm => vm.GenreId, map => map.MapFrom(m => m.Genre.Id))
            .ForMember(vm => vm.IsAvailable, map => map.MapFrom(m => m.Stocks.Any(s => s.IsAvailable)))
            .ForMember(vm => vm.NumberOfStocks, map => map.MapFrom(m => m.Stocks.Count))
            .ForMember(vm => vm.Image, map => map.MapFrom(m => string.IsNullOrEmpty(m.Image) == true ? "unknown.jpg" : m.Image));

            Mapper.CreateMap<Genre, GenreViewModel>()
                .ForMember(vm => vm.NumberOfMovies, map => map.MapFrom(g => g.Movies.Count()))
                .ForMember(vm => vm.Movie, map => map.MapFrom(g => g.Movies))
                ;

            // code omitted
            Mapper.CreateMap<Profiler, CustomerViewModel>();

            Mapper.CreateMap<Stock, StockViewModel>();

            Mapper.CreateMap<Rental, RentalViewModel>();

            Mapper.CreateMap<Image, ImageViewModel>();

            Mapper.CreateMap<Group, GroupViewModel>()
                    .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.GroupUser, map => map.MapFrom(m => m.GroupUsers))
                    ;
            Mapper.CreateMap<Role, RoleViewModel>()
                    .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))
                    ;

            Mapper.CreateMap<User, UserPermissionViewModel>()
                    .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.UserRoles, map => map.MapFrom(m => m.UserRoles))
                    ;
            Mapper.CreateMap<User, UserViewModel>()
                    .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                    ;
            Mapper.CreateMap<User, UserCalendarViewModel>()
                   .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                   .ForMember(vm => vm.Username, map => map.MapFrom(m => m.Username))
                   .ForMember(vm => vm.Color, map => map.MapFrom(m => m.Profiler.Where(x => x.UserId.Value == m.Id).ToList().Count > 0 ? m.Profiler.Where(x => x.UserId.Value == m.Id).ToList()[0].Color : null))
            ;
            Mapper.CreateMap<Calendar, CalendarViewModel>()
                    .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.title, map => map.MapFrom(m => (m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList().Count > 0) ? m.Description + "(" + m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList()[0].School.Name.ToString() + ")" : m.Description))
                    .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
                    .ForMember(vm => vm.start, map => map.MapFrom(m => m.StartAt.ToString("yyyy-MM-ddTHH:mm:ss")))
                    .ForMember(vm => vm.end, map => map.MapFrom(m => m.EndAt.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss")))
                    .ForMember(vm => vm.borderColor, map => map.MapFrom(m => m.borderColor))
                    .ForMember(vm => vm.backgroundColor, map => map.MapFrom(m => m.backgroundColor))
                    .ForMember(vm => vm.resourceId, map => map.MapFrom(m => HomeCinema.Data.Common.common.Generate((m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList().Count > 0) ? m.Description + "(" + m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList()[0].School.Name.ToString() + ")" : m.Description)))
                    ;

            Mapper.CreateMap<Calendar, CalendarGroupsViewModel>()
                   //.ForMember(vm => vm.Id, map => map.MapFrom(m => HomeCinema.Data.Common.common.Generate(m.Id.ToString(), (m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList().Count > 0) ? m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList()[0].SchoolId.ToString() : null)))
                   .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                   .ForMember(vm => vm.title, map => map.MapFrom(m => m.Description))
                   .ForMember(vm => vm.Name, map => map.MapFrom(m => (m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList().Count > 0) ? m.CalenderLession.Where(x => x.CalendarId == m.Id).ToList()[0].School.Name : null))
                   ;

            Mapper.CreateMap<Calendar, CalendarViewDetailModel>()
                   .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))
                   .ForMember(vm => vm.title, map => map.MapFrom(m => m.Description))
                   .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
                   .ForMember(vm => vm.DayOfWeek, map => map.MapFrom(m => m.StartAt.DayOfWeek + "(" + m.StartAt.ToString("dd/MM/yyyy") + ")"))
                   .ForMember(vm => vm.start, map => map.MapFrom(m => m.StartAt.ToString("HH : mm")))
                   .ForMember(vm => vm.end, map => map.MapFrom(m => m.EndAt.ToString("HH : mm")))
                   .ForMember(vm => vm.Time, map => map.MapFrom(m => Infrastructure.Extensions.Common.DayOfWeekvi(m.StartAt.DayOfWeek) + " (" + m.StartAt.ToString("dd/MM/yyyy") + ") từ " + m.StartAt.ToString("HH:mm") + " - " + m.EndAt.ToString("HH:mm")))
                   .ForMember(vm => vm.CalenderLession, map => map.MapFrom(m => m.CalenderLession))
                   .ForMember(vm => vm.soluongchau, map => map.MapFrom(m => m.Soluongchau));
            ;
            Mapper.CreateMap<Calendar, CalendarLessionViewModel>()
                  .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))
                    .ForMember(vm => vm.title, map => map.MapFrom(m => m.Description))
                    .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
                    .ForMember(vm => vm.start, map => map.MapFrom(m => m.StartAt.ToString("yyyy-MM-ddTHH:mm:ss")))
                    .ForMember(vm => vm.end, map => map.MapFrom(m => m.EndAt.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss")))
                    .ForMember(vm => vm.borderColor, map => map.MapFrom(m => m.borderColor))
                    .ForMember(vm => vm.backgroundColor, map => map.MapFrom(m => m.backgroundColor))
            ;
            Mapper.CreateMap<Sys_Unit, Sys_UnitViewModel>()
                   .ForMember(vm => vm.UnitID, map => map.MapFrom(m => m.UnitID))
                   ;

            Mapper.CreateMap<Sys_User, Sys_UserViewModel>()

                    .ForMember(vm => vm.UserID, map => map.MapFrom(m => m.UserID))

                    ;
            Mapper.CreateMap<Key_Sys_Unit, Key_Sys_UnitViewModel>()

                  .ForMember(vm => vm.ID, map => map.MapFrom(m => m.Id))

                  .ForMember(vm => vm.Key, map => map.MapFrom(m => m.Key))

                  ;

            Mapper.CreateMap<Lession, LessionViewModel>()
                 .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                 .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description + "(" + m.StartAt.ToString(@"hh\:mm") + "-" + m.EndAt.ToString(@"hh\:mm") + ")"))
                 ;

            Mapper.CreateMap<Lession, LessionaddViewModel>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))

                ;

            Mapper.CreateMap<Classroom, ClassroomViewModel>()
               .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))

               ;
            Mapper.CreateMap<School, SchoolViewModel>()
               .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
               .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
               ;

            Mapper.CreateMap<Role, UserRoleViewModel>()
               .ForMember(vm => vm.RoleID, map => map.MapFrom(m => m.Id))
               .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
               .ForMember(vm => vm.Description, map => map.MapFrom(m => m.Description))
               ;
        }
    }
}