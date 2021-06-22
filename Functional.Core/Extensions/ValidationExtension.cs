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

    }
}
