using Continaer_Project.Models;

namespace Continaer_Project.Services;

/// <summary>
/// Keeps everything in memory so the sample runs with no database. Data resets whenever
/// the app restarts. To persist it, implement <see cref="IPropertyService"/> on top of
/// Entity Framework Core and change the one registration line in Program.cs.
/// </summary>
public class InMemoryPropertyService : IPropertyService
{
    private readonly object _lock = new();
    private readonly List<Property> _properties;
    private readonly List<Inquiry> _inquiries = new();
    private int _nextPropertyId;
    private int _nextInquiryId = 1;

    public InMemoryPropertyService()
    {
        _properties = SeedData.Create();
        _nextPropertyId = _properties.Max(p => p.Id) + 1;
    }

    public PagedResult<Property> Search(PropertyFilter filter)
    {
        IEnumerable<Property> query = Snapshot();

        if (!string.IsNullOrWhiteSpace(filter.Q))
        {
            var term = filter.Q.Trim();
            query = query.Where(p =>
                Contains(p.Title, term) ||
                Contains(p.Address, term) ||
                Contains(p.City, term) ||
                Contains(p.Description, term));
        }

        if (!string.IsNullOrWhiteSpace(filter.City))
        {
            query = query.Where(p => string.Equals(p.City, filter.City, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.Listing.HasValue)
        {
            query = query.Where(p => p.ListingType == filter.Listing.Value);
        }

        if (filter.Kind.HasValue)
        {
            query = query.Where(p => p.PropertyType == filter.Kind.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        if (filter.Beds.HasValue)
        {
            query = query.Where(p => p.Bedrooms >= filter.Beds.Value);
        }

        query = filter.Sort switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "area_desc" => query.OrderByDescending(p => p.AreaSqFt),
            _ => query.OrderByDescending(p => p.CreatedUtc)
        };

        var all = query.ToList();
        var pageSize = Math.Clamp(filter.PageSize, 1, 50);
        var totalPages = Math.Max(1, (int)Math.Ceiling(all.Count / (double)pageSize));
        var page = Math.Clamp(filter.PageNumber, 1, totalPages);
        var items = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PagedResult<Property>(items, all.Count, page, pageSize);
    }

    public Property? GetById(int id) => Snapshot().FirstOrDefault(p => p.Id == id);

    public IReadOnlyList<Property> GetFeatured(int count) =>
        Snapshot()
            .Where(p => p.IsFeatured)
            .OrderByDescending(p => p.CreatedUtc)
            .Take(count)
            .ToList();

    public IReadOnlyList<Property> GetSimilar(Property property, int count) =>
        Snapshot()
            .Where(p => p.Id != property.Id
                        && p.ListingType == property.ListingType
                        && (p.City == property.City || p.PropertyType == property.PropertyType))
            .OrderByDescending(p => p.City == property.City)
            .ThenBy(p => Math.Abs(p.Price - property.Price))
            .Take(count)
            .ToList();

    public IReadOnlyList<string> GetCities() =>
        Snapshot()
            .Select(p => p.City)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(c => c)
            .ToList();

    public PropertyStats GetStats()
    {
        var all = Snapshot();
        return new PropertyStats(
            all.Count,
            all.Count(p => p.ListingType == ListingType.Sale),
            all.Count(p => p.ListingType == ListingType.Rent));
    }

    public Property Add(ListingInput input)
    {
        lock (_lock)
        {
            var property = new Property
            {
                Id = _nextPropertyId++,
                Title = input.Title.Trim(),
                Description = input.Description.Trim(),
                Address = input.Address.Trim(),
                City = input.City.Trim(),
                ListingType = input.ListingType,
                PropertyType = input.PropertyType,
                Price = input.Price,
                Bedrooms = input.Bedrooms,
                Bathrooms = input.Bathrooms,
                AreaSqFt = input.AreaSqFt,
                ImageUrl = string.IsNullOrWhiteSpace(input.ImageUrl) ? null : input.ImageUrl.Trim(),
                IsFeatured = false,
                CreatedUtc = DateTime.UtcNow
            };

            _properties.Add(property);
            return property;
        }
    }

    public Inquiry? AddInquiry(int propertyId, InquiryInput input)
    {
        lock (_lock)
        {
            var property = _properties.FirstOrDefault(p => p.Id == propertyId);
            if (property is null)
            {
                return null;
            }

            var inquiry = new Inquiry
            {
                Id = _nextInquiryId++,
                PropertyId = property.Id,
                PropertyTitle = property.Title,
                Name = input.Name.Trim(),
                Email = input.Email.Trim(),
                Phone = string.IsNullOrWhiteSpace(input.Phone) ? null : input.Phone.Trim(),
                Message = input.Message.Trim(),
                CreatedUtc = DateTime.UtcNow
            };

            _inquiries.Add(inquiry);
            return inquiry;
        }
    }

    public IReadOnlyList<Inquiry> GetInquiries()
    {
        lock (_lock)
        {
            return _inquiries.OrderByDescending(i => i.CreatedUtc).ToList();
        }
    }

    private List<Property> Snapshot()
    {
        lock (_lock)
        {
            return _properties.ToList();
        }
    }

    private static bool Contains(string source, string term) =>
        source.Contains(term, StringComparison.OrdinalIgnoreCase);
}
