using Functional.App.Entities;

namespace Functional.App.Interfaces
{
    public interface ISwiftService
    {
        void Wire(MakeTransfer transfer, AccountState state);
    }
}
