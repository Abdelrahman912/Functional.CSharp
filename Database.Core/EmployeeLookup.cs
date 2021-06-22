using Functional.Core;
using Functional.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Core
{
   public static class EmployeeLookup
    {
        public static void Run()
        {
            ConnectionString connString = "my-database";

            SqlTemplate select = "SELECT * FROM EMPLOYEES"
                , sqlById = $"{select} WHERE ID = @Id"
                , sqlByName = $"{select} WHERE LASTNAME = @LastName";

            var queryEmployees = connString.Query<Employee>();

            var queryById = queryEmployees.Apply(sqlById);

            var queryByLastName = queryEmployees.Apply(sqlByName);

            Option<Employee> LookupEmployee(Guid id) =>
                queryById(new {Id=id }).FirstOrDefault();

            IEnumerable<Employee> FindEmployeesByLastName(string lastName)=>
                queryByLastName(new {LastName = lastName });

        }
    }
}
