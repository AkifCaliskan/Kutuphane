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
    public class OperationsService : IOperationsService
    {
        private IOperationsDal _operationsDal;
        private IQueryableRepository<Operations> _queryableDal;

        public OperationsService(IOperationsDal operationsDal, IQueryableRepository<Operations> queryableDal)
        {
            _operationsDal = operationsDal;
            _queryableDal = queryableDal;
        }

        public Operations Add(Operations operations)
        {
            return _operationsDal.Add(operations);
        }

        public List<Operations> GetAll()
        {
            return _operationsDal.GetList();
        }

        public Operations GetById(int id)
        {
            return _operationsDal.Get(x => x.OperationsID == id);
        }

        public IQueryable<Operations> GetQueryable(bool status)
        {
            return _queryableDal.Table.Where(x=> x.OperationStatus == status);
        }

        public IQueryable<Operations> GetQueryable(Expression<Func<Operations, bool>> filter = null)
        {
            return _queryableDal.Table;
        }

        public IQueryable<Operations> GetQueryableSearch()
        {
            return _queryableDal.Table;
        }

        public Operations Update(Operations operations)
        {
            return _operationsDal.Update(operations);
        }

        public void UpdateDeletedColumn(int id)
        {
            var deletedItem = GetById(id);
            Update(deletedItem);
        }
    }
}
