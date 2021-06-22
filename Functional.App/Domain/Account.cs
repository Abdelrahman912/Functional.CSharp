using Functional.Core;
using OnlineBank.Core.Domain.Entities;
using static Functional.Core.Extensions.OptionExtension;

namespace OnlineBank.Core.Domain
{
    public static class Account
    {

        public static Option<AccountState> Debit(this AccountState acc , decimal amount)
        {
            return acc.Balance < amount ? None :Some(new AccountState((acc.Balance - amount)));
        }

    }
}
