using Functional.Core.Errors;
using System;
using System.Collections.Generic;
using Unit = System.ValueTuple;

namespace Functional.Core.Extensions
{
    public static class ValidationExtension
    {
        public static Validation<T> Valid<T>(T value) => new Validation<T>(value);

        // create a Validation in the Invalid state
        public static Validation.Invalid Invalid(params Error[] errors) => new Validation.Invalid(errors);
        public static Validation<R> Invalid<R>(params Error[] errors) => new Validation.Invalid(errors);
        public static Validation.Invalid Invalid(IEnumerable<Error> errors) => new Validation.Invalid(errors);
        public static Validation<R> Invalid<R>(IEnumerable<Error> errors) => new Validation.Invalid(errors);

        public static Validation<R> Map<T, R>(this Validation<T> validation, Func<T, R> f) =>
           validation.Match(errors => Invalid(errors),
                            data => Valid(f(data)));

        public static Validation<Unit> ForEach<T>(this Validation<T> validation, Action<T> action) =>
           validation.Match(errors => Invalid(errors),
                            data => Valid(action.ToFunc().Invoke(data)));

        public static Validation<R> Bind<T, R>(this Validation<T> validation, Func<T, Validation<R>> f) =>
            validation.Match(errors => Invalid(errors),
                             data => f(data));

        public static Validation<R> Apply<T, R>(this Validation<Func<T, R>> valF, Validation<T> valT) =>
            valF.Match(errF => Invalid(errF),
                       f => valT.Match(
                           errT => Invalid(errT),
                           t => Valid(f(t))));

        public static Validation<Func<T2, R>> Apply<T1, T2, R>(this Validation<Func<T1, T2, R>> valF, Validation<T1> valT) =>
            valF.Map(f => f.Curry()).Apply(valT);

        public static Validation<Func<T2,T3,R>> Apply<T1,T2,T3,R>(this Validation<Func<T1, T2,T3, R>> valF, Validation<T1> valT)=>
            valF.Map(f=>f.CurryFirst()).Apply(valT);


    }
}
