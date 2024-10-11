using Autofac;
using Kutuphane.Bussiness.Abstract;
using Kutuphane.Bussiness.Concrete.Services;
using Kutuphane.DataAccess.Abstract;
using Kutuphane.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Bussiness.DependencyResolvers.AutoFac
{
   public class BussinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<OperationsService>().As<IOperationsService>();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<EfOperationsDal>().As<IOperationsDal>();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>();
            builder.RegisterType<EfBookDal>().As<IBookDal>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<EfAuthorDal>().As<IAuthorDal>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
        }
    }
}
