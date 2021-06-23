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

        
        public static Option<T> Some<T>(T value) =>
            new Option.Some<T>(value);

        public static Option<R> Map<T, R>(this Option.None _, Func<T, R> f) =>
                None;

        public static Option<R> Map<T, R>(this Option.Some<T> some, Func<T, R> f) =>
           f(some.Value);

        public static Option<R> Map<T, R>(this Option<T> optT, Func<T, R> f) =>
           optT.Match<Option<R>>(() => None,
                                 (t) => Some(f(t)));

        public static Option<R> Select<T, R>(this Option<T> optT, Func<T, R> f) =>
            optT.Map(f);

        public static Option<Unit> ForEach<T>(this Option<T> option, Action<T> action) =>
            option.Map(action.ToFunc());


        public static Option<R> Bind<T, R>(this Option<T> option, Func<T, Option<R>> f) =>
            option.Match(() => None,
                        (t) => f(t));

        public static Option<RR> SelectMany<T,R,RR>(this Option<T> option,Func<T,Option<R>> bind , Func<T,R,RR> project)=>
            option.Match(()=>None,
                         t=>bind(t).Match<Option<RR>>(
                             ()=>None,
                             r=> project(t,r)));

        public static Option<T> Return<T>(T t) =>
            Some(t);

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> pred) =>
            option.Match(() => None,
                        (t) => pred(t) ? option: None);

        public static IEnumerable<R> Bind<T, R>(this Option<T> option, Func<T, IEnumerable<R>> func) =>
            option.AsEnumerable().SelectMany(t => func(t));


        public static Option<R> Apply<T, R>(this Option<Func<T, R>> optF, Option<T> optT) =>
            optF.Match(() => None,
                       f => optT.Match<Option<R>>(
                           () => None,
                           value => f(value)));


        public static Option<Func<T2, R>> Apply<T1, T2, R>(this Option<Func<T1, T2, R>> optF, Option<T1> optT) =>
            optF.Map(f => f.Curry()).Apply(optT);

        public static Option<Func<T2,T3, R>> Apply<T1, T2,T3, R>(this Option<Func<T1, T2,T3, R>> optF, Option<T1> optT) =>
           optF.Map(f => f.CurryFirst()).Apply(optT);


        public static Option<T> OrElse<T>(this Option<T> left, Option<T> right) =>
            left.Match(() => right,
                        _ => left);

        public static Option<T> OrElse<T>(this Option<T> option, Func<Option<T>> fallback) =>
            option.Match(() => fallback(),
                         _ => option);

        public static T GetOrElse<T>(this Option<T> left, T right) =>
           left.Match(() => right,
                       t => t);

        public static T GetOrElse<T>(this Option<T> option, Func<T> fallback) =>
            option.Match(() => fallback(),
                         t => t);

    }
}
