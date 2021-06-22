using Functional.Core;
using Functional.Core.DTOS;
using Functional.Core.Errors;
using Functional.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using OnlineBank.Core.Domain.Entities;
using OnlineBank.Core.Domain.Errors;
using System;
using System.Text.RegularExpressions;
using static Functional.Core.Functional;
using static Functional.Core.Extensions.EitherExtension;
using static Functional.Core.Extensions.ValidationExtension;
using Unit = System.ValueTuple;
using System.Linq;
using static Functional.Core.Extensions.OptionExtension;

namespace Functional.App.Controllers
{

    public class BookTransferController:ControllerBase
    {

       

        private readonly DateTime _now = DateTime.Now;

        private readonly Func<BookTransfer,Validation<BookTransfer>> _validator;

        private readonly Func<BookTransfer, Exceptional<Unit>> _save;

        public BookTransferController(Func<BookTransfer,Validation<BookTransfer>> validator,
                                      Func<BookTransfer,Exceptional<Unit>> save)
        {
            _validator = validator;
            _save = save;
        }

        [HttpPost, Route("api/transfers/future")]
        public ResultDto<Unit> BookTransfer2([FromBody] BookTransfer request) =>
           Handle2(request).Match(Invalid: errors => new ResultDto<Unit>(errors.First()),
                                  Valid: result => result.Match(
                                   Exception: _ => new ResultDto<Unit>(Errors.ServerError),
                                   Success: unit => new ResultDto<Unit>(unit)));

        //[HttpPost, Route("api/transfers/future")]
        //public ResultDto<Unit> BookTransfer([FromBody] BookTransfer request) =>
        //    Handle(request).ToResult();

        //private Either<Error,Unit> Handle(BookTransfer cmd)=>
        //    ValidateBic(cmd)
        //        .Bind(ValidateDate)
        //        .Bind(Save);

        //private Either<Error,BookTransfer> ValidateBic(BookTransfer cmd)
        //{
        //    if (!_bicRegex.IsMatch(cmd.Bic))
        //        return Errors.InvalidBic;
        //    else return cmd;

        //}

        //private Either<Error,BookTransfer> ValidateDate(BookTransfer cmd)
        //{
        //    if (cmd.Date.Date <= _now.Date)
        //        return Errors.TransferDataIsPast;
        //    else return cmd;
        //}

        private Either<Error,Unit> Save(BookTransfer cmd)
        {
            return default(Unit);
        }

        private Validation<Exceptional<Unit>> Handle2(BookTransfer cmd) =>
         _validator(cmd)
              .Map(_save);

        //private Exceptional<Unit> Save2(BookTransfer cmd)
        //{
        //    try
        //    {
        //        //Some Side effect logic
        //    }
        //    catch (Exception exp)
        //    {
        //        return exp;
        //    }
        //    return Unit();
        //}


    }
}
