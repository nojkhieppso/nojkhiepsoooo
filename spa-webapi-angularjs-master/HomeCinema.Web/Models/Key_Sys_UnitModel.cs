using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class Key_Sys_UnitViewModel
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Encodedomain { get; set; }
        public int UnitID { get; set; }
    }
}