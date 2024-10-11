using Kutuphane.Core.DataAccess.EntityFramework;
using Kutuphane.DataAccess.Abstract;
using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DataAccess.Concrete.EntityFramework
{
   public class EfOperationsDal: EfEntityRepositoryBase<Operations, Context>, IOperationsDal
    {
    }
}
