namespace Functional.App.Entities
{
    public class AccountState
    {
        public decimal Balance { get; }

        public AccountState(decimal balance)
        {
            Balance = balance;
        }

    }
}
