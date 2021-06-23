using System;

namespace Functional.Core.Extensions
{
    public static class FuncExtension
    {

        public static Func<R> Map<T, R>(this Func<T> f, Func<T, R> g) =>
            () => g(f());

        public static Func<R> Bind<T, R>(this Func<T> f, Func<T, Func<R>> g) =>
            g(f());

        public static R Apply<T, R>(this Func<T, R> func, T t) =>
           func(t);

        public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> func,T1 t1) =>
            t2 => func(t1, t2);

        public static Func<T2,T3, R> Apply<T1, T2,T3, R>(this Func<T1, T2,T3, R> func, T1 t1) =>
            (t2,t3) => func(t1, t2,t3);


        public static Func<T1,Func<T2,R>> Curry<T1,T2,R>(this Func<T1,T2,R> func)=>
            t1=>t2=> func(t1, t2);

        public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> func) =>
            t1 => t2 => t3 => func(t1, t2, t3);

        public static Func<T1, Func<T2, T3, R>> CurryFirst<T1, T2, T3, R>(this Func<T1, T2, T3, R> func) =>
            t1 => (t2 , t3) => func(t1, t2, t3);

    }
}
