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
    public class AuthorService : IAuthorService
    {
        private IAuthorDal _authorDal;
        private IQueryableRepository<Author> _queryableRepository;
        public AuthorService(IAuthorDal authorDal, IQueryableRepository<Author> queryableRepository)
        {
            _authorDal = authorDal;
            _queryableRepository = queryableRepository;
        }

        public Author Add(Author authorizedDomain)
        {
            return _authorDal.Add(authorizedDomain);
        }

        public List<Author> GetAll()
        {
            return _authorDal.GetList();
        }

        public Author GetById(int id)
        {
            return _authorDal.Get(x => x.AuthorID == id);
        }

        public IQueryable<Author> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(x=>x.status == status);
        }

        public IQueryable<Author> GetQueryable(Expression<Func<Author, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<Author> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public Author Update(Author author)
        {
            return _authorDal.Update(author);
        }

        public void UpdateDeletedColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
