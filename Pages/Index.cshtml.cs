using Continaer_Project.Models;
using Continaer_Project.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Continaer_Project.Pages;

public class IndexModel : PageModel
{
    private readonly IPropertyService _properties;

    public IndexModel(IPropertyService properties)
    {
        _properties = properties;
    }

    public IReadOnlyList<Property> Featured { get; private set; } = Array.Empty<Property>();
    public IReadOnlyList<string> Cities { get; private set; } = Array.Empty<string>();
    public PropertyStats Stats { get; private set; } = new(0, 0, 0);

    public void OnGet()
    {
        Featured = _properties.GetFeatured(6);
        Cities = _properties.GetCities();
        Stats = _properties.GetStats();
    }
}
