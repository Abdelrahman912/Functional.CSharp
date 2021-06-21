namespace Functional.App.Interfaces
{
    public interface IValidator<T>
    {
        bool IsValid(T t);
    }
}
