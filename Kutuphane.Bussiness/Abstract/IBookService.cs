using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Abstract
{
    public interface IBookService
    {
        List<Book> GetAll();
        IQueryable<Book> GetQueryable(bool status);
        IQueryable<Book> GetQueryable(Expression<Func<Book, bool>> filter = null);
        IQueryable<Book> GetQueryableSearch();
        Book GetById(int id);
        Book Add(Book book);
        Book Update(Book book);
        void UpdateDeletedColumn(int id);
    }
}
