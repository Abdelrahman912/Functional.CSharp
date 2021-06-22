using Functional.Core;
using Functional.Core.DTOS;
using Functional.Core.Errors;
using Functional.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using OnlineBank.Core.Domain.Entities;
using OnlineBank.Core.Domain.Errors;
using System;
using System.Text.RegularExpressions;
using static Functional.Core.Extensions.EitherExtension;
using Unit = System.ValueTuple;

namespace Functional.App.Controllers
{
    public class BookTransferController:ControllerBase
    {
        private readonly Regex _bicRegex = new Regex("[A-Z]{11}");

        private readonly DateTime _now = DateTime.Now;

        [HttpPost, Route("api/transfers/future")]
        public ResultDto<Unit> BookTransfer([FromBody] BookTransfer request) =>
            Handle(request).ToResult();

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
