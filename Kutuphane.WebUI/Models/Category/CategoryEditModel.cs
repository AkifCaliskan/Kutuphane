using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Models.Category
{
    public class CategoryEditModel
    {
        public int CategoryID { get; set; }
        public bool CategoryStatus { get; set; }
        public string CategoryName { get; set; }
    }
}
