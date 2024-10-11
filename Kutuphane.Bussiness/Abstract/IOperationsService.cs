using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
   public interface IOperationsService
    {
        List<Operations> GetAll();
        IQueryable<Operations> GetQueryable(bool status);
        IQueryable<Operations> GetQueryable(Expression<Func<Operations, bool>> filter = null);
        IQueryable<Operations> GetQueryableSearch();
        Operations GetById(int id);
        Operations Add(Operations operations);
        Operations Update(Operations operations );
        void UpdateDeletedColumn(int id);

    }
}
