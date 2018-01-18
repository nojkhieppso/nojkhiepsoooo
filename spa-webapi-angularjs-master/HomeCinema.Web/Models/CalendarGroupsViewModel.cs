using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class CalendarGroupsViewModel
    {
        public string Id { get; set; }
        public string title { get; set; }
        public string Name { get; set; }
    }
}