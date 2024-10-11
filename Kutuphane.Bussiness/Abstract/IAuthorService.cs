using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
    public interface IAuthorService
    {
        List<Author> GetAll();
        IQueryable<Author> GetQueryable(bool status);
        IQueryable<Author> GetQueryable(Expression<Func<Author, bool>> filter = null);
        IQueryable<Author> GetQueryableSearch();
        Author GetById(int id);
        Author Add(Author author);
        Author Update(Author author);
        void UpdateDeletedColumn(int id);
    }
}
