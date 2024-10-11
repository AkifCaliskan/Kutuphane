using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class BookEditModel
    {
        public string BookImage { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string SerialNumber { get; set; }
        public string PageCount { get; set; }
        public string PublicationYear { get; set; }
        public string YayinYili { get; set; }
    }
}
