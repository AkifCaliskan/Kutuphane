using Kutuphane.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Entities.Concrete
{
    public class Operations : IEntity
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int OperationsID { get; set; }
        public DateTime İssueDate { get; set; }
        public DateTime ReturnTime { get; set; }
        public bool OperationStatus { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }

        //Alt tablo
        //public virtual Banner Banner { get; set; }

        //public virtual ICollection<BannerTranslation> BannerTranslations { get; set; }
        //public Banner()
        //{
        //    this.BannerTranslations = new List<BannerTranslation>();
        //}
    }
}
