using Kutuphane.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Entities.Concrete
{
    public class Category:IEntity
    {
      

        public int CategoryID { get; set; }

        public bool CategoryStatus { get; set; }
        public string CategoryName { get; set; }
        public List<Book> Books { get; set; }
    }
}
