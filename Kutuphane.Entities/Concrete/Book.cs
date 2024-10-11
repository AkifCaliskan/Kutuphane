using Kutuphane.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Entities.Concrete
{
    public class Book : IEntity
    {
        public string YayinYili { get; set; }
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookDescription { get; set; }
        public string SerialNumber { get; set; }
        public string PageCount { get; set; }
        public string PublicationYear { get; set; }
        public bool Status { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public Author Author { get; set; }
        public Category Category { get; set; }
        public List<Operations> Operationss { get; set; }

       
    }
}
