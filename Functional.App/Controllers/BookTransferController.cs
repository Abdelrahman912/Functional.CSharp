using Functional.App.Domain.Entities;
using Functional.App.Domain.Errors;
using Functional.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Functional.Core.Either;
using Unit = System.ValueTuple;
using static Functional.Core.Extensions.EitherExtension;
using  Functional.Core.Extensions;

namespace Functional.App.Controllers
{
    public class BookTransferController
    {
        private readonly Regex _bicRegex = new Regex("[A-Z]{11}");

        private readonly DateTime _now = DateTime.Now;

        private Either<Error,Unit> Handle(BookTransfer cmd)=>
            ValidateBic(cmd)
                .Bind(ValidateDate)
                .Bind(Save);

        private Either<Error,BookTransfer> ValidateBic(BookTransfer cmd)
        {
            if (!_bicRegex.IsMatch(cmd.Bic))
                return Errors.InvalidBic;
            else return cmd;

        }

        private Either<Error,BookTransfer> ValidateDate(BookTransfer cmd)
        {
            if (cmd.Date.Date <= _now.Date)
                return Errors.TransferDataIsPast;
            else return cmd;
        }

        private Either<Error,Unit> Save(BookTransfer cmd)
        {
            return default(Unit);
        }

    }
}
