using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class AuthorListModel
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public bool status { get; set; }
    }
}
