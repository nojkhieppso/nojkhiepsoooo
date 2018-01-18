using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class CalendarViewDetailModel
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
        public string DayOfWeek { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string Time { get; set; }
        public double TienGiang { get; set; }
        public int soluongchau { get; set; }
        public List<CalenderLession> CalenderLession { get; set; }
    }
}