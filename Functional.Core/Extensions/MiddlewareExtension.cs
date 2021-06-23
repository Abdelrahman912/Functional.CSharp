using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Core.Extensions
{
   public static class MiddlewareExtension
    {
        public static T Run<T>(this Middleware<T> mw) => (T)mw(t => t);

        public static Middleware<R> Map<T, R>(this Middleware<T> mw, Func<T, R> f) =>
          cont=>  mw(t=>cont(f(t)));

        public static Middleware<R> Bind<T, R>(this Middleware<T> mw, Func<T, Middleware<R>> f) =>
           cont=>  mw(t=>f(t)(cont));

    }
}
