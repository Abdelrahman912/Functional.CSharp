using System;
using System.Data;
using System.Data.SqlClient;
using static Functional.Core.Functional;

namespace Database.Core
{
    public static class ConnectionHelper
    {
        public static R Connect<R>(string connString, Func<IDbConnection, R> func) =>
            Using(new SqlConnection(connString),
                conn => func(conn));
        

    }
}
