using System;
using Functional.Core;
using Functional.Core.Extensions;

namespace Middleware.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Pyramid of Doom
            //Setup("setup Instance",
            //    () => LogTime("Log Instance",
            //        () => Connect("DB-Connection string", Query)));

            Middleware<string> setup = f => Setup("setup Instance", ()=>f("time"));
            Func<string,Middleware<string>> logTime =str => f => LogTime("log Instance", () => f("log"));
            Func<string, Middleware<string>> connect=str=>  f => Connect("DB-connection string", f);
            Func<string, Employee> query = Query;
            setup.Bind(logTime).Bind( connect).Map(query).Run();

        }

        public static Employee Query(string dbInst)
        {
            Console.WriteLine("Getting Employee");
            return new Employee("Hamada");
        }


        public static T LogTime<T>(string loggerInst, Func<T> f)
        {
            Console.WriteLine("Start Logging Time");
            var result = f();
            Console.WriteLine("End Logging Time");
            return result;
        }

        public static T Setup<T>(string setupinst , Func<T> f)
        {
            Console.WriteLine("Start Setup");
            var result = f();
            Console.WriteLine("End Setup");
            return result;
        }

        public static R Connect<R>(string connString, Func<string, R> func)
        {
            Console.WriteLine("Connect To Database");
           var result =  func("DB instance");
            Console.WriteLine("Remove connection to DB");
            return result;
        }

    }
}
