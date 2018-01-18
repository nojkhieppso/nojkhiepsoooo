namespace HomeCinema.Genecode.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sys_Unit
    {
       
        public int UnitID { get; set; }

        public int? ParentUnitID { get; set; }

        
        public string Language { get; set; }

        
        public string Code { get; set; }

        
        public string Name { get; set; }

        public int? UnitType { get; set; }

        
        public string Address { get; set; }

        
        public string Tel { get; set; }

        
        public string Fax { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string Infor { get; set; }

        public string FileAttach { get; set; }

        public string FileName { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
