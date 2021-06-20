using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Unit = System.ValueTuple;

namespace Functional.Core.Extensions
{
    public static class CollectionExtension
    {
        public static IEnumerable<Unit> ForEach<T>(this IEnumerable<T> ts, Action<T> action) =>
            ts.Select(action.ToFunc()).ToImmutableList();

        public static IEnumerable<T> List<T>(params T[] items) =>
            items.ToImmutableList();

        public static IEnumerable<T> Return<T>(params T[] items)=>
            List(items);


        public static IEnumerable<R> Bind<T, R>(this IEnumerable<T> ts, Func<T, Option<R>> func) =>
            ts.SelectMany(t => func(t).AsEnumerable());

    }
}
