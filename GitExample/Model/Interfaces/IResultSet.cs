
using System.Collections.Generic;

namespace Interfaces
{

    public interface IResultSet<T>

    {
        IEnumerable<T> Items { get; set; }

        Pager Pager { get; }
    }
}