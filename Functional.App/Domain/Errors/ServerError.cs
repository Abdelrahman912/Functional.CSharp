using Functional.Core.Errors;

namespace OnlineBank.Core.Domain.Errors
{
    public class ServerError:Error
    {
        public override string Message =>
          "Server Error, Status Code: 500";
    }
}
