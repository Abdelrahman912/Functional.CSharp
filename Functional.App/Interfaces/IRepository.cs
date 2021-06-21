using Functional.Core;
using System;

namespace Functional.App.Interfaces
{
    public interface IRepository<T>
    {
        Option<T> Get(Guid id);

        void Save(Guid id, T t);
    }

}
