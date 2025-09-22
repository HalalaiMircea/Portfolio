using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Portfolio.Services;

public class IconRegistry(HttpClient client, IJSRuntime js)
{
    private readonly Dictionary<string, string> _idAspectRatioMap = new();

    public List<string> IconIds => _idAspectRatioMap.Keys.ToList();

    public async Task LoadAsync()
    {
        var doc = new XmlDocument();
        doc.LoadXml(await client.GetStringAsync("symbol/svg/sprite.css.svg"));
        foreach (XmlNode node in doc.GetElementsByTagName("symbol"))
        {
            // Attributes list cannot be null... old API
            string id = node.Attributes!["id"]!.Value;
            string[] viewBoxParts = node.Attributes["viewBox"]!.Value.Split(' ');
            _idAspectRatioMap[id] = $"calc({viewBoxParts[^2]}/{viewBoxParts[^1]})";
        }
        // inject raw sprite sheet into DOM
        string svgXML = doc.GetElementsByTagName("svg")[0]!.OuterXml;
        await js.InvokeVoidAsync("embedSpriteSheet", svgXML);

        Console.WriteLine("SVG Sprite sheet IDs: " + string.Join(" ", IconIds));
    }

    public MarkupString GetIcon(string id, string? height = "auto")
    {
        if (!id.StartsWith("i-"))
        {
            id = "i-" + id;
        }
        if (!_idAspectRatioMap.TryGetValue(id, out string? ar))
        {
            throw new ArgumentException($"Icon with Symbol ID '{id}' is not registered.");
        }
        return new MarkupString(
            $"""
             <svg style='aspect-ratio:{ar};height:{height}'>
                <use href='#{id}'></use>
             </svg>
             """
        );/*symbol/svg/sprite.css.svg*/
    }
}
