namespace Continaer_Project.Models;

public enum ListingType
{
    Sale,
    Rent
}

public enum PropertyType
{
    Apartment,
    House,
    Villa,
    Plot,
    Commercial
}

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public ListingType ListingType { get; set; }
    public PropertyType PropertyType { get; set; }

    /// <summary>Price in rupees. For rentals this is the monthly rent.</summary>
    public long Price { get; set; }

    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int AreaSqFt { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
