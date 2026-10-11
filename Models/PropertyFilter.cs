namespace Continaer_Project.Models;

public class PropertyFilter
{
    public string? Q { get; set; }
    public string? City { get; set; }
    public ListingType? Listing { get; set; }
    public PropertyType? Kind { get; set; }
    public long? MinPrice { get; set; }
    public long? MaxPrice { get; set; }
    public int? Beds { get; set; }
    public string Sort { get; set; } = "newest";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 9;
}

public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize)
{
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
}

public record PropertyStats(int Total, int ForSale, int ForRent);
