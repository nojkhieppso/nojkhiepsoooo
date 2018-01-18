namespace HomeCinema.Genecode.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_User
    {
        
        public string UserID { get; set; }

        
        public string UserName { get; set; }

        
        public string LoginName { get; set; }

        public short? Role { get; set; }

        public short? UsedState { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Year { get; set; }

        
        public string Tel { get; set; }

        
        public string Email { get; set; }

        public DateTime? ExpiryDate { get; set; }

        
        public string Infor { get; set; }

        
        public string ImagePath { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public int? IsOnline { get; set; }

        public DateTime? LastPasswordChangedDate { get; set; }

        public DateTime? LastIPAddress { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public int? FailedLoginAttemptCount { get; set; }

        public int? iType { get; set; }

        
        public string sType { get; set; }

        
        public string FieldExtension { get; set; }
    }
}
