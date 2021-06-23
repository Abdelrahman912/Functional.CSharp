using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Core.Extensions
{
    public static class TryExtension
    {
        public static Exceptional<T> Run<T>(this Try<T> @try)
        {
            try { return @try(); }
            catch (Exception exp) { return exp; }
        }

        public static Try<R> Map<T, R>(this Try<T> @try, Func<T, R> f) =>
            () => @try.Run()
                      .Match<Exceptional<R>>(exp => exp,
                                             t => f(t));

        public static Try<R> Bind<T, R>(this Try<T> @try, Func<T, Try<R>> f) =>
           () => @try.Run()
                     .Match(exp => exp,
                            t => f(t).Run());
    }
}
