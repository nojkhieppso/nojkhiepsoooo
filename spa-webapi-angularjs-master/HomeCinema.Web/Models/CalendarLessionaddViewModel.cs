using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class CalendarLessionaddViewModel
    {
        public int Id { get; set; }
        public int CalenderId { get; set; }
        public int ClassId { get; set; }
        public int LessionId { get; set; }
    }
}