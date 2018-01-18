using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class iv_v_phConfiguration : iv_v_phBaseConfiguration<iv_v_ph>
    {
        public iv_v_phConfiguration()
        {
            Property(m => m.ma).IsRequired();
            Property(m => m.ma_kh).IsRequired();
            Property(m => m.ten).IsRequired();
            Property(m => m.tg).IsRequired();
            Property(m => m.hthuc).IsRequired();
        }
    }
}
