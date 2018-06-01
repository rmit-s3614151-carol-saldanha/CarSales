using System.Linq;

namespace Interfaces
{
    public interface IPageHelper<T>
    {
        IResultSet<T> GetPage(IQueryable<T> items, int pageNumber);
    }
}