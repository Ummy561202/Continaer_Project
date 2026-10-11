using Continaer_Project.Models;

namespace Continaer_Project.Services;

public interface IPropertyService
{
    PagedResult<Property> Search(PropertyFilter filter);
    Property? GetById(int id);
    IReadOnlyList<Property> GetFeatured(int count);
    IReadOnlyList<Property> GetSimilar(Property property, int count);
    IReadOnlyList<string> GetCities();
    PropertyStats GetStats();
    Property Add(ListingInput input);

    /// <summary>Returns null when the property does not exist.</summary>
    Inquiry? AddInquiry(int propertyId, InquiryInput input);

    IReadOnlyList<Inquiry> GetInquiries();
}
