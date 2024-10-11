using Kutuphane.Core.DataAccess;
using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DataAccess.Abstract
{
   public interface IBookDal:IEntityRepository<Book>
    {
    }
}
