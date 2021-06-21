using Functional.App.Domain;
using Functional.App.Entities;
using Functional.App.Interfaces;
using Functional.Core.Extensions;
using static Functional.Core.Extensions.OptionExtension;

namespace Functional.App.Controllers
{
    public class MakeTransferController
    {

        private readonly IValidator<MakeTransfer> _validator;
        private readonly IRepository<AccountState> _accounts;
        private readonly ISwiftService _swift;

        public MakeTransferController(IValidator<MakeTransfer> validator,
                                      IRepository<AccountState> accounts,
                                      ISwiftService swift)
        {
            _validator = validator;
            _accounts = accounts;
            _swift = swift;
        }

        public void MakeTransfer(MakeTransfer transfer)
        {
            Some(transfer).Map(Normalize)
                          .Where(_validator.IsValid)
                          .ForEach(Book);
        }

        public void Book(MakeTransfer transfer)
        {
            _accounts.Get(transfer.DebitedAccountId)
                     .Bind(account => account.Debit(transfer.Amount))
                     .ForEach(account =>
                     {
                         _accounts.Save(transfer.DebitedAccountId,account);
                         _swift.Wire(transfer,account);
                     });
            //Book
        }

        public MakeTransfer Normalize(MakeTransfer transfer)
        {
            //Normalize the transfer
            return new MakeTransfer();
        }

    }
}
