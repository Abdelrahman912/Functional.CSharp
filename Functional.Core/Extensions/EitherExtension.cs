using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functional.Core;
using Functional.Core.DTOS;
using Functional.Core.Errors;
using  Unit = System.ValueTuple;

namespace Functional.Core.Extensions
{
    public static class EitherExtension
    {
        public static Either.Left<L> Left<L>(L l) => new Either.Left<L>(l);
        public static Either.Right<R> Right<R>(R r) => new Either.Right<R>(r);

        public static Either<L, T> Map<L, R, T>(this Either<L, R> either, Func<R, T> func) =>
            either.Match<Either<L,T>>(left =>Left(left),
                                      right =>Right(func(right)));

        public static Either<LL, RR> Map<L, LL, R, RR>(this Either<L, R> either,Func<L,LL> leftFunc, Func<R, RR> rightFunc) =>
           either.Match<Either<LL,RR>>(left => Left(leftFunc(left)),
                                     right => Right(rightFunc(right)));

        public static Either<L, Unit> ForEach<L, R>(this Either<L, R> either, Action<R> action) =>
            either.Match<Either<L, Unit>>(left => Left(left),
                                         right => Right(action.ToFunc().Invoke(right)));


        public static Either<L, T> Bind<L, R, T>(this Either<L, R> either, Func<R, Either<L,T>> func) =>
            either.Match(left => Left(left),
                         right => func(right));


        public static ResultDto<T> ToResult<T>(this Either<Error, T> either) =>
            either.Match(error => new ResultDto<T>(error),
                         data => new ResultDto<T>(data));

    }
}
