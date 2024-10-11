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
    public class CategoryService : ICategoryService
    {
        private ICategoryDal _categoryDal;
        private IQueryableRepository<Category> _queryableDal;
        public CategoryService(ICategoryDal categoryDal, IQueryableRepository<Category> queryableRepository)
        {
            _categoryDal = categoryDal;
            _queryableDal = queryableRepository;
        }


        public Category Add(Category category)
        {
           return _categoryDal.Add(category);
        }

        public List<Category> GetAll()
        {
           return _categoryDal.GetList();
        }

        public Category GetById(int id)
        {
          return  _categoryDal.Get(x => x.CategoryID == id);
        }

        public IQueryable<Category> GetQueryable(bool status)
        {
           return _queryableDal.Table.Where(x=> x.CategoryStatus == status);
        }

        public IQueryable<Category> GetQueryable(Expression<Func<Category, bool>> filter = null)
        {
            return _queryableDal.Table;
        }

        public IQueryable<Category> GetQueryableSearch()
        {
           return _queryableDal.Table;
        }

        public Category Update(Category category)
        {
           return _categoryDal.Update(category);
        }

        public void UpdateDeletedColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
