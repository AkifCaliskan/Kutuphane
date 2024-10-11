using Kutuphane.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Entities.Concrete
{
    public class Author : IEntity
    {
    

        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public bool status { get; set; }


    }
}
