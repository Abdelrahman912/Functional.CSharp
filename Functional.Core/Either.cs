using System;
using static Functional.Core.Either;

namespace Functional.Core
{
    public struct Either<L, R>
    {
        internal L Left { get; }
        internal R Right { get; }

        private readonly bool _isRight;
        private readonly bool _isLeft => !_isRight;

        internal Either(L left)
        {
            _isRight = false;
            Left = left;
            Right = default(R);
        }

        internal Either(R right)
        {
            Right = right;
            _isRight = true;
            Left = default(L);
        }

        public static implicit operator Either<L, R>(L left) => new Either<L, R>(left);
        public static implicit operator Either<L, R>(R right) => new Either<L, R>(right);

        public static implicit operator Either<L, R>(Left<L> left) => new Either<L, R>(left.Value);
        public static implicit operator Either<L, R>(Right<R> right) => new Either<L, R>(right.Value);

        public T Match<T>(Func<L, T> left, Func<R, T> right) =>
            _isLeft ? left(Left) : right(Right);

    }

    public static class Either
    {


        public struct Left<L>
        {
            internal L Value { get; }
            internal Left(L value)
            {
                Value = value;
            }

            public override string ToString() =>
                $"Left({Value})";

        }

        public struct Right<R>
        {
            internal R Value { get; }
            internal Right(R value)
            {
                Value = value;
            }

            public override string ToString() =>
                $"Right({Value})";

        }
    }
}
