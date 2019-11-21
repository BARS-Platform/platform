using System.Collections.Generic;
using Microsoft.FSharp.Collections;

namespace Platform.Services.Helpers
{
    public static class FSharpListHelper
    {
        public static FSharpList<T> ToFSharpList<T>(this IEnumerable<T> enumerable)
        {
            using var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            var fSharpList = GenerateSubList(enumerator);
            return fSharpList;
        }

        private static FSharpList<T> GenerateSubList<T>(IEnumerator<T> enumerator)
        {
            var currentValue = enumerator.Current;
            return enumerator.MoveNext()
                ? new FSharpList<T>(currentValue, GenerateSubList(enumerator))
                : new FSharpList<T>(currentValue, FSharpList<T>.Empty);
        }
    }
}