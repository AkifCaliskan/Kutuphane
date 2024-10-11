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
    public class UserService : IUserService
    {
        private IUserDal _userDal;
        private IQueryableRepository<User> _queryableRepository;
        public UserService(IUserDal userDal, IQueryableRepository<User> queryableRepository)
        {
            _userDal = userDal;
            _queryableRepository = queryableRepository;
        }
        public User Add(User user)
        {
            return _userDal.Add(user);
        }

        public List<User> GetAll()
        {
            return _userDal.GetList();
        }

       

        public User GetByID(int id)
        {
            return _userDal.Get(x => x.UserID == id);
        }

        public IQueryable<User> GetQueryable(bool status)
        {
            return _queryableRepository.Table.Where(p=> p.UserStatus == status);
        }

        public IQueryable<User> GetQueryable(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? _queryableRepository.Table : _queryableRepository.Table.Where(filter);
        }

        public IQueryable<User> GetQueryableSearch()
        {
            return _queryableRepository.Table;
        }

        public User Update(User user)
        {
            return _userDal.Update(user);

        }

        public void UpdateDeleteColumn(int id)
        {
            var deletedItem = GetByID(id);
            Update(deletedItem);
        }
    }
}
