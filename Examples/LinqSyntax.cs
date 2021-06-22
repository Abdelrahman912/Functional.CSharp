using Functional.Core.Extensions;
using System;
using System.Linq;
using static Functional.Core.Extensions.EitherExtension;
using static Functional.Core.Extensions.OptionExtension;
using static Functional.Core.Extensions.ValidationExtension;

namespace Examples
{
    public static class LinqSyntax
    {

        public static void Demo()
        {
            Func<int, int, int> multiply = (x, y) => x * y;

            Some(multiply).Apply(Some(3)).Apply(Some(2));
            multiply.Apply(3).Apply(4);
            Some(3).Bind(Return);
            var result = from i in Enumerable.Range(1, 100)
                         where i % 20 == 0
                         orderby -i
                         select $"{i}%";

            Some(3).Map(x => x * 2);

           var optResult =  from x in Some(3)
                            select x * 2;
            Some(3).Bind(x => Some(4).Map(y => x + y));

            var optSum = from x in Some(3)
                         from y in Some(4)
                         select x + y;
            Some(new Func<int, int, int>((x, y) => x + y))
                .Apply(Some(3))
                .Apply(Some(4));

        }
    }
}
