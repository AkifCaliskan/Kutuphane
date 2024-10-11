using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class BookTrackingEditModel
    {
        public int OperationsID { get; set; }
        public DateTime İssueDate { get; set; }
        public DateTime? ReturnTime { get; set; }
        public bool OperationStatus { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

    }
}
