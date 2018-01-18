using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Core
{
    public class PaginationSetExtention<T>
    {

        public int Page { get; set; }

        public int Count
        {
            get
            {
                return (null != this.Items) ? this.Items.Count() : 0;
            }
        }

        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public string Totalcurrency { get; set; }
        public string Totalmonth { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}