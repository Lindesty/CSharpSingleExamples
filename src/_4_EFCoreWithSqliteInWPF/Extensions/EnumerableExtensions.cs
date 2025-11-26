using System;
using System.Collections;
using System.Collections.Generic;

namespace _4_EFCoreWithSqliteInWPF.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<TSource>(this IEnumerable<TSource> sources, Action<TSource> action)
    {
        foreach (var source in sources) action(source);
    }
}