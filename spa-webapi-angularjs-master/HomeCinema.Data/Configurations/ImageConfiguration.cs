using HomeCinema.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Configurations
{
    public class ImageConfiguration : EntityBaseConfiguration<Image>
    {
        public ImageConfiguration()
        {
            
            Property(m => m.FileLength).IsRequired();
            Property(m => m.FileName).IsRequired().HasMaxLength(100);
            Property(m => m.LocalFilePath).IsRequired().HasMaxLength(250);
           
        }
    }
}
