using Kutuphane.Bussiness.Abstract;
using Kutuphane.Core.DataAccess;
using Kutuphane.DataAccess.Abstract;
using Kutuphane.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.Concrete.Services
{
    public class BookService : IBookService
    {
        private IBookDal _bookDal;
        private IQueryableRepository<Book> _queryableDal;
        public BookService(IBookDal bookDal, IQueryableRepository<Book> queryableRepository)
        {
            _bookDal = bookDal;
            _queryableDal = queryableRepository;
        }
        public Book Add(Book book)
        {
           return _bookDal.Add(book);
        }

        public List<Book> GetAll()
        {
            return _bookDal.GetList();
        }

        public Book GetById(int id)
        {
            return _bookDal.Get(x => x.BookID == id);
        }

        public IQueryable<Book> GetQueryable(bool status)
        {
            return _queryableDal.Table.Where(p=> p.Status == status);
        }

        public IQueryable<Book> GetQueryable(Expression<Func<Book, bool>> filter = null)
        {
             return _queryableDal.Table;
        }

        public IQueryable<Book> GetQueryableSearch()
        {
           return _queryableDal.Table;
        }

        public Book Update(Book book)
        {
            return _bookDal.Update(book);
        }

        public void UpdateDeletedColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
