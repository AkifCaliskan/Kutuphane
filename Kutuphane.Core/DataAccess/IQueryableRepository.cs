using System.Linq;
using Kutuphane.Core.Entities;

namespace Kutuphane.Core.DataAccess
{
    public interface IQueryableRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Table { get; }
    }
}
