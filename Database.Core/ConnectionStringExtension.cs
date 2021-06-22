using Dapper;
using System;
using System.Collections.Generic;
using static Database.Core.ConnectionHelper;

namespace Database.Core
{
    public static class ConnectionStringExtension
    {
        public static Func<SqlTemplate, object, IEnumerable<T>> Query<T>(this ConnectionString connString) =>
            (sql, param) => Connect(connString, conn => conn.Query<T>(sql, param));
    }
}
