using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
   public interface ICategoryService
    {
        List<Category> GetAll();
        IQueryable<Category> GetQueryable(bool status);
        IQueryable<Category> GetQueryable(Expression<Func<Category, bool>> filter = null);
        IQueryable<Category> GetQueryableSearch();
        Category GetById(int id);
        Category Add(Category category);
        Category Update(Category categpry);
        void UpdateDeletedColumn(int id);


    }
}
