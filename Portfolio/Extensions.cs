using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Portfolio;

public static class Extensions
{
    /// Uses the path -> namespace identical naming convention to determine the path of the scoped JS file
    public static async ValueTask<IJSObjectReference> ImportScopedModule<TComponent>(this IJSRuntime jsRuntime)
        where TComponent : ComponentBase
    {
        string[] pathSegments = typeof(TComponent).FullName!.Split('.');
        pathSegments[0] = ".";
        pathSegments[^1] += ".razor.js";
        return await jsRuntime.InvokeAsync<IJSObjectReference>("import", string.Join('/', pathSegments));
    }
}

[AttributeUsage(AttributeTargets.Field)]
public sealed class FriendlyNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

public static class EnumExtensions
{
    public static string GetFriendlyName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?
            .GetCustomAttributes(typeof(FriendlyNameAttribute), false)
            .FirstOrDefault() as FriendlyNameAttribute;
        return attr?.Name ?? value.ToString();
    }
}
