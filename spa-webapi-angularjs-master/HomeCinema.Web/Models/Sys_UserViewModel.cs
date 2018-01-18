using HomeCinema.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HomeCinema.Web.Models
{
    public class Sys_UserViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public int Role { get; set; }
        public int UsedState { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Year { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string ExpiryDate { get; set; }
        public string Infor { get; set; }
        public string ImagePath { get; set; }
        public string IsOnline { get; set; }
        public string LastPasswordChangedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string sType { get; set; }
        public string FieldExtension { get; set; }
        public DateTime LastIPAddress { get; set; }
        public int FailedLoginAttemptCount { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int iType { get; set; }
    }
}