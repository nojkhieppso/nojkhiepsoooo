using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class CalendarLessionViewModel
    {
        public Guid ID { get; set; }
        public string title { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
        public Guid? lessionID { get; set; }
        public Guid? ClassID { get; set; }
        public Guid? SchoolId { get; set; }
        public Guid CalendarId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string today { get; set; }
        public int? soluongchau { get; set; }
    }
}