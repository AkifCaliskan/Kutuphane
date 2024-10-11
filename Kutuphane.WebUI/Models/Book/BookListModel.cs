using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebUI.Models.Book
{
    public class BookListModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SeriNo { get; set; }
        public string BasimYili { get; set; }
        public string SayfaSayisi { get; set; }
    }
}
