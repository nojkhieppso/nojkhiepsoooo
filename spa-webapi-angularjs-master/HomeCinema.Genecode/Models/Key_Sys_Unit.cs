namespace HomeCinema.Genecode.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Key_Sys_Unit
    {
        public int ID { get; set; }

        public string Key { get; set; }

        public string Encodedomain { get; set; }

        public int? UnitID { get; set; }
    }
}
