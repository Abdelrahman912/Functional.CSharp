﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unit = System.ValueTuple;

namespace Functional.Core.Extensions
{
    public static class OptionExtension
    {
        public static Option.None None =>
            Option.None.Default;

        
        public static Option.Some<T> Some<T>(T value) =>
            new Option.Some<T>(value);

        public static Option<R> Map<T, R>(this Option.None _, Func<T, R> f) =>
                None;

        public static Option<R> Map<T, R>(this Option.Some<T> some, Func<T, R> f) =>
           f(some.Value);

        public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f) =>
           optT.Match<Option<R>>(() => None,
                                 (t) => Some(f(t)));

        public static Option<Unit> ForEach<T>(this Option<T> option, Action<T> action) =>
            option.Map(action.ToFunc());


        public static Option<R> Bind<T, R>(this Option<T> option, Func<T, Option<R>> f) =>
            option.Match(() => None,
                        (t) => f(t));

        public static Option<T> Return<T>(T t) =>
            Some(t);

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> pred) =>
            option.Match(() => None,
                        (t) => pred(t) ? option: None);

        public static IEnumerable<R> Bind<T, R>(this Option<T> option, Func<T, IEnumerable<R>> func) =>
            option.AsEnumerable().SelectMany(t => func(t));


    }
}
