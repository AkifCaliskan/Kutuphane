using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class BookTrackingModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int OperationsID { get; set; }
        public DateTime İssueDate { get; set; }
        public DateTime ReturnTime { get; set; }
        public string IsReturnTime { get; set; }
        public string BookName { get; set; }
        public string UserName { get; set; }
        public bool OperationStatus { get; set; }
    }
}
