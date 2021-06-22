using Functional.Core;
using OnlineBank.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Functional.Core.Functional;
using static Functional.Core.Extensions.ValidationExtension;
using OnlineBank.Core.Domain.Errors;
using Unit = System.ValueTuple;

namespace OnlineBank.Core.Domain
{
    public static class ValidationHelper
    {
        private static Validation<BookTransfer> ValidateBic2(BookTransfer cmd)
        {
            var _bicRegex = new Regex("[A-Z]{11}");

            if (!_bicRegex.IsMatch(cmd.Bic))
                return Invalid(Errors.Errors.InvalidBic);
            else return Valid(cmd);

        }

        private static Validation<BookTransfer> ValidateDate2(BookTransfer cmd)
        {
            if (cmd.Date.Date <= DateTime.Now.Date)
                return Invalid(Errors.Errors.TransferDataIsPast);
            else return Valid(cmd);
        }

        public static Validation<BookTransfer> Validate(BookTransfer cmd) =>
            ValidateBic2(cmd).Bind(ValidateDate2);

    }
}
