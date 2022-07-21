using System.Reflection;

namespace ToDoAsLessCodeAsPossible.Domain.Tests;

public static class GetPrivateValueExtension
{
    public static TPropertyType? GetPrivateValue<TPropertyType>(this object obj, string name)
    {
        var propertyInfo = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
        if (propertyInfo == null)
        {
            throw new Exception("Can not find property info for field name " + name);
        }
        
        return (TPropertyType?)propertyInfo.GetValue(obj);
    }
}