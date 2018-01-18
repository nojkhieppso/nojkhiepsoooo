using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Donghuong : IEntityBase {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string Xom { get; set; }
        public string Congviec { get; set; }
        public string Diachi { get; set; }
        public string Dienthoai { get; set; }
        public string Email { get; set; }
        public string Acount { get; set; }
        public string Pass { get; set; }
        public int? GroupNewsId { get; set; }
        public DateTime? Date { get; set; }
    }
}
