using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class iv_v_phBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        //public iv_v_phBaseConfiguration()
        //{
        //    HasKey(e => e.ma);
        //}
    }
}
