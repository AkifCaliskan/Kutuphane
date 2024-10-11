using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Models.Operations
{
    public class BookGiveModel
    {
        public int OperationsID { get; set; }
        public DateTime İssueDate { get; set; }
        public string ReturnTime { get; set; }
        public bool OperationStatus { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        

    }
}
