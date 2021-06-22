namespace Functional.App.Domain.Errors
{
    public class TransferDataIsPast:Error
    {
        public override string Message =>
            "Transfer date cannot be in the past";
    }
}
