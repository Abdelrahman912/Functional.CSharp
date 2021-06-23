using System;

namespace Functional.Core
{
    public delegate dynamic Middleware<T>(Func<T,dynamic> cont);
}
