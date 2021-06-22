using System;
using Unit = System.ValueTuple;

namespace Functional.Core.Extensions
{
    public static class ExceptionalExtension
    {
        public static Exceptional<T> Exceptional<T>(T value) => new Exceptional<T>(value);


        public static Exceptional<R> Map<T, R>(this Exceptional<T> exceptional, Func<T, R> f) =>
            exceptional.Match<Exceptional<R>>(exp => exp,
                                              data => f(data));

        public static Exceptional<Unit> ForEach<T>(this Exceptional<T> exceptional, Action<T> action) =>
           exceptional.Match<Exceptional<Unit>>(exp => exp,
                                             data => action.ToFunc().Invoke(data));

        public static Exceptional<R> Bind<T, R>(this Exceptional<T> exceptional, Func<T, Exceptional<R>> f) =>
           exceptional.Match(exp => exp,
                             data => f(data));

    }
}
