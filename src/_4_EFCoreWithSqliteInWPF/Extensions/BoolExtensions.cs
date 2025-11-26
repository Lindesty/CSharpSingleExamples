using System;
using System.IO.Pipelines;

namespace _4_EFCoreWithSqliteInWPF.Extensions;

public static class BoolExtensions
{
    public static bool Then(this bool result, Action action)
    {
        if (result) action.Invoke();
        return result;
    }

    public static bool Then(this bool result, Func<bool> func)
    {
        return result && func.Invoke();
    }

}