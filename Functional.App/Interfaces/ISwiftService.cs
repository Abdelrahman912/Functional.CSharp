using OnlineBank.Core.Domain.Entities;

namespace Functional.App.Interfaces
{
    public interface ISwiftService
    {
        void Wire(MakeTransfer transfer, AccountState state);
    }
}
