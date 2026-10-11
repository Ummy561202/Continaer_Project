using System.ComponentModel.DataAnnotations;

namespace Continaer_Project.Models;

/// <summary>
/// What the "List a property" form posts. Kept separate from <see cref="Property"/>
/// so a visitor can never set the Id, the featured flag or the created date.
/// </summary>
public class ListingInput
{
    [Required, StringLength(120, MinimumLength = 5)]
    public string Title { get; set; } = "";

    [Required, StringLength(4000, MinimumLength = 20)]
    public string Description { get; set; } = "";

    [Required, StringLength(200)]
    public string Address { get; set; } = "";

    [Required, StringLength(80)]
    public string City { get; set; } = "";

    [Display(Name = "Listing type")]
    public ListingType ListingType { get; set; }

    [Display(Name = "Property type")]
    public PropertyType PropertyType { get; set; }

    [Range(1, 100_000_000_000), Display(Name = "Price (₹, monthly rent for rentals)")]
    public long Price { get; set; }

    [Range(0, 50)]
    public int Bedrooms { get; set; }

    [Range(0, 50)]
    public int Bathrooms { get; set; }

    [Range(1, 1_000_000), Display(Name = "Area (sq ft)")]
    public int AreaSqFt { get; set; }

    [Url, StringLength(500), Display(Name = "Image URL (optional)")]
    public string? ImageUrl { get; set; }
}
