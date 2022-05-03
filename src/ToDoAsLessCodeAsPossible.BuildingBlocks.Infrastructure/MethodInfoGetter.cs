using System.Reflection;

namespace ToDoAsLessCodeAsPossible.BuildingBlocks.Infrastructure;

internal static class MethodInfoGetter
{
    public static MethodInfo? GetByName(object handler, string methodName)
    {
        var methods = handler.GetType().GetMethods();
        var handleMethod = methods.FirstOrDefault(x => x.Name == methodName);

        return handleMethod;
    }
}