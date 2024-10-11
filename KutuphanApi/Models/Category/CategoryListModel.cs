using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class CategoryListModel
    {
        public int CategoryID { get; set; }
        public bool CategoryStatus { get; set; }
        public string CategoryName { get; set; }
    }
}
