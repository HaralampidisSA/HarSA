using System.Collections.Generic;

namespace HarSA.Infrastructure
{
    public interface IPagedList<T> : IList<T>, IPagedData
    {
    }
}
