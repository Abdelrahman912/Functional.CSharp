using Functional.App.Domain.Entities;
using Functional.Core;
using static Functional.Core.Extensions.OptionExtension;

namespace Functional.App.Domain
{
    public static class Account
    {

        public static Option<AccountState> Debit(this AccountState acc , decimal amount)
        {
            return acc.Balance < amount ? None :Some(new AccountState((acc.Balance - amount)));
        }

    }
}
