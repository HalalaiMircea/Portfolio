using System.Xml;
using Microsoft.AspNetCore.Components;

namespace Portfolio.Services;

public class IconRegistry(HttpClient client)
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
        Console.WriteLine("SVG Sprite sheet IDs: " + string.Join(" ", IconIds));
    }

    public MarkupString GetIcon(string id, string? height = "auto")
    {
        if (!_idAspectRatioMap.TryGetValue(id, out string? ar))
        {
            throw new ArgumentException($"Icon with Symbol ID '{id}' is not registered.");
        }
        return new MarkupString(
            $"""
             <svg style='aspect-ratio:{ar};height:{height}'>
                <use href='symbol/svg/sprite.css.svg#{id}'></use>
             </svg>
             """
        );
    }
}
