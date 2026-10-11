using System.Globalization;
using Continaer_Project.Models;
using Continaer_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Continaer_Project.Pages.Listings;

public class IndexModel : PageModel
{
    private readonly IPropertyService _properties;

    public IndexModel(IPropertyService properties)
    {
        _properties = properties;
    }

    [BindProperty(SupportsGet = true)] public string? Q { get; set; }
    [BindProperty(SupportsGet = true)] public string? City { get; set; }
    [BindProperty(SupportsGet = true)] public ListingType? Listing { get; set; }
    [BindProperty(SupportsGet = true)] public PropertyType? Kind { get; set; }
    [BindProperty(SupportsGet = true)] public long? MinPrice { get; set; }
    [BindProperty(SupportsGet = true)] public long? MaxPrice { get; set; }
    [BindProperty(SupportsGet = true)] public int? Beds { get; set; }
    [BindProperty(SupportsGet = true)] public string Sort { get; set; } = "newest";
    [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;

    public PagedResult<Property> Result { get; private set; } = new(Array.Empty<Property>(), 0, 1, 9);
    public IReadOnlyList<string> Cities { get; private set; } = Array.Empty<string>();

    public void OnGet()
    {
        Result = _properties.Search(new PropertyFilter
        {
            Q = Q,
            City = City,
            Listing = Listing,
            Kind = Kind,
            MinPrice = MinPrice,
            MaxPrice = MaxPrice,
            Beds = Beds,
            Sort = Sort ?? "newest",
            PageNumber = PageNumber
        });

        Cities = _properties.GetCities();
    }

    /// <summary>Route values for a pagination link that keeps the current filters.</summary>
    public Dictionary<string, string> PageRoute(int page)
    {
        var route = new Dictionary<string, string>
        {
            ["PageNumber"] = page.ToString(CultureInfo.InvariantCulture)
        };

        if (!string.IsNullOrWhiteSpace(Q)) route["Q"] = Q.Trim();
        if (!string.IsNullOrWhiteSpace(City)) route["City"] = City;
        if (Listing.HasValue) route["Listing"] = Listing.Value.ToString();
        if (Kind.HasValue) route["Kind"] = Kind.Value.ToString();
        if (MinPrice.HasValue) route["MinPrice"] = MinPrice.Value.ToString(CultureInfo.InvariantCulture);
        if (MaxPrice.HasValue) route["MaxPrice"] = MaxPrice.Value.ToString(CultureInfo.InvariantCulture);
        if (Beds.HasValue) route["Beds"] = Beds.Value.ToString(CultureInfo.InvariantCulture);
        if (!string.IsNullOrWhiteSpace(Sort) && Sort != "newest") route["Sort"] = Sort;

        return route;
    }
}
