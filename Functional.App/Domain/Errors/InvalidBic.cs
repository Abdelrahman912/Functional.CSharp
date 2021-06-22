namespace Functional.App.Domain.Errors
{
    public class InvalidBic:Error
    {
        public override string Message =>
           "The beneficiary's BIC/SWIFT code is invalid";
    }
}
