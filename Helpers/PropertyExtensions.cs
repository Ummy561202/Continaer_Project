using System.Globalization;
using System.Text;
using Continaer_Project.Models;

namespace Continaer_Project.Helpers;

public static class PropertyExtensions
{
    /// <summary>"₹1.35 Cr", "₹78 L" for sales; "₹45,000/mo" for rentals.</summary>
    public static string DisplayPrice(this Property property) =>
        property.ListingType == ListingType.Rent
            ? $"₹{Group(property.Price)}/mo"
            : Compact(property.Price);

    public static string PricePerSqFt(this Property property) =>
        property.AreaSqFt > 0
            ? $"₹{Group(property.Price / property.AreaSqFt)} / sq ft"
            : "";

    /// <summary>"3 Beds · 3 Baths · 1,650 sq ft" (beds and baths are skipped when zero).</summary>
    public static string Summary(this Property property)
    {
        var parts = new List<string>();
        if (property.Bedrooms > 0)
        {
            parts.Add(property.Bedrooms == 1 ? "1 Bed" : $"{property.Bedrooms} Beds");
        }

        if (property.Bathrooms > 0)
        {
            parts.Add(property.Bathrooms == 1 ? "1 Bath" : $"{property.Bathrooms} Baths");
        }

        parts.Add($"{Group(property.AreaSqFt)} sq ft");
        return string.Join(" · ", parts);
    }

    public static string Compact(long price)
    {
        if (price >= 10_000_000)
        {
            return $"₹{Number(price / 10_000_000d)} Cr";
        }

        if (price >= 100_000)
        {
            return $"₹{Number(price / 100_000d)} L";
        }

        return $"₹{Group(price)}";
    }

    /// <summary>Indian digit grouping: 1234567 becomes "12,34,567".</summary>
    public static string Group(long value)
    {
        var digits = Math.Abs(value).ToString(CultureInfo.InvariantCulture);
        var sign = value < 0 ? "-" : "";
        if (digits.Length <= 3)
        {
            return sign + digits;
        }

        var tail = digits.Substring(digits.Length - 3);
        var head = digits.Substring(0, digits.Length - 3);
        var sb = new StringBuilder();
        for (var end = head.Length; end > 0; end -= 2)
        {
            var start = Math.Max(0, end - 2);
            if (sb.Length > 0)
            {
                sb.Insert(0, ',');
            }

            sb.Insert(0, head.Substring(start, end - start));
        }

        return sign + sb + "," + tail;
    }

    private static string Number(double value) =>
        value.ToString("0.##", CultureInfo.InvariantCulture);
}
