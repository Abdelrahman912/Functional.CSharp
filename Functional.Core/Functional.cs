using static Functional.Core.Extensions.OptionExtension;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit = System.ValueTuple;

namespace Functional.Core
{
    public static class  Functional
    {
        public static Unit Unit() => default(Unit);

        public static Option<string> Lookup(this NameValueCollection @this, string key) =>
          @this[key];

        public static Option<T> Lookup<K, T>(this IDictionary<K, T> dict, K key)
        {
            T value;
            return dict.TryGetValue(key, out value) ? Some(value) : None;
        }

        public static R Using<TDisp, R>(TDisp disposable
        , Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (var disp = disposable) return func(disp);
        }


    }
}
