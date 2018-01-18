using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class CalendarViewModel
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string UserID { get; set; }
        public string Description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string today { get; set; }
        public string resourceId { get; set; }
    }
}