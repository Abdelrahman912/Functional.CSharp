namespace Functional.Core.Extensions
{
    public static class OptionExtension
    {
        public static Option.None None =>
            Option.None.Default;

        public static Option.Some<T> Some<T>(T value) =>
            new Option.Some<T>(value);
    }
}
