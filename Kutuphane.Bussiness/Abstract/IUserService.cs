using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
   public interface IUserService
    {
        IQueryable<User> GetQueryable(bool status);
        IQueryable<User> GetQueryable(Expression<Func< User, bool>> filter = null);
        IQueryable<User> GetQueryableSearch();
        User GetByID(int id);
        User Add(User user);
        void UpdateDeleteColumn(int id);
        User Update(User user);

    }
}
