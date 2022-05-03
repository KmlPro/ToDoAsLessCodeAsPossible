using System.Reflection;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure;

internal static class AsyncMethodInfoExecutor
{
    public static async Task<TResult> InvokeAsync<TResult>(MethodInfo methodInfo, object targetObject, params object[] parameters)
    {
        dynamic awaitable = methodInfo.Invoke(targetObject, parameters)!;
        await awaitable;

        return (TResult)awaitable.GetAwaiter().GetResult();
    }
}