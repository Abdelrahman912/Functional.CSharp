namespace OnlineBank.Core.Domain.Errors
{
    //Factory for creating specific subclasses of Error.
    public static class Errors
    {
        public static ServerError ServerError =>
            new ServerError();

        public static InvalidBic InvalidBic =>
            new InvalidBic();

        public static TransferDataIsPast TransferDataIsPast => 
            new TransferDataIsPast();    
    }
}
