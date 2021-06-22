
using Functional.Core.Errors;

namespace Functional.Core.DTOS
{
    public class ResultDto<T>
    {
        public bool Succeeded { get; }
        public bool Failed => !Succeeded;
        public T Data { get; }
        public Error Error { get; }
        public ResultDto(T data) { Succeeded = true; Data = data; }
        public ResultDto(Error error) { Error = error; }
    }
   
}
