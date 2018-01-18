using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HomeCinema.Entities {
    
    public class Config : IEntityBase {
        public int Id { get; set; }
        public string MailSmtp { get; set; }
        public short? MailPort { get; set; }
        public string MailInfo { get; set; }
        public string MailNoreply { get; set; }
        public string MailPassword { get; set; }
        public string PlaceHead { get; set; }
        public string PlaceBody { get; set; }
        public string GoogleId { get; set; }
        public string Contact { get; set; }
        public string Copyright { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Lang { get; set; }
    }
}
