using Continaer_Project.Models;

namespace Continaer_Project.Services;

/// <summary>Sample listings. All names, places and prices are made up for the demo.</summary>
internal static class SeedData
{
    public static List<Property> Create()
    {
        var now = DateTime.UtcNow;
        var nextId = 1;

        Property P(
            string title, string city, string address,
            ListingType listing, PropertyType kind, long price,
            int beds, int baths, int area, bool featured, int daysAgo, string description) =>
            new()
            {
                Id = nextId++,
                Title = title,
                City = city,
                Address = address,
                ListingType = listing,
                PropertyType = kind,
                Price = price,
                Bedrooms = beds,
                Bathrooms = baths,
                AreaSqFt = area,
                IsFeatured = featured,
                CreatedUtc = now.AddDays(-daysAgo),
                Description = description
            };

        return new List<Property>
        {
            P("3 BHK Apartment with Balcony Views", "Bengaluru", "Sarjapur Road",
                ListingType.Sale, PropertyType.Apartment, 13_500_000, 3, 3, 1650, true, 1,
                "Bright corner apartment on a high floor with two balconies and a modular kitchen.\n" +
                "Gated community with a clubhouse, covered parking and 24x7 power backup."),

            P("Furnished 2 BHK in Indiranagar", "Bengaluru", "Indiranagar, 12th Main",
                ListingType.Rent, PropertyType.Apartment, 45_000, 2, 2, 1100, true, 2,
                "Fully furnished flat close to cafes, the metro and tech offices.\n" +
                "Available immediately for an 11-month lease. Pets allowed."),

            P("4 BHK Villa with Private Garden", "Hyderabad", "Gachibowli",
                ListingType.Sale, PropertyType.Villa, 38_500_000, 4, 4, 3200, true, 3,
                "Independent villa in a gated layout with a landscaped garden, home office and a two-car garage.\n" +
                "Walking distance to schools and the financial district."),

            P("Compact 1 BHK near the IT Park", "Pune", "Hinjewadi Phase 1",
                ListingType.Rent, PropertyType.Apartment, 18_000, 1, 1, 550, false, 4,
                "Well-kept 1 BHK ideal for a single professional. Shuttle access to major offices.\n" +
                "Society has a gym and a play area."),

            P("Sea-facing 3 BHK Apartment", "Mumbai", "Worli",
                ListingType.Sale, PropertyType.Apartment, 95_000_000, 3, 3, 1900, true, 5,
                "Premium high-rise apartment with uninterrupted sea views, a private lift lobby and two reserved parking slots.\n" +
                "Amenities include a pool, sky lounge and concierge."),

            P("Grade-A Office Space on OMR", "Chennai", "OMR, Perungudi",
                ListingType.Rent, PropertyType.Commercial, 120_000, 0, 2, 2400, false, 6,
                "Open-plan office floor with a reception area, two meeting rooms and a pantry.\n" +
                "Ample parking and a short walk from the bus corridor."),

            P("Residential Plot in a Gated Community", "Hyderabad", "Shamshabad",
                ListingType.Sale, PropertyType.Plot, 6_200_000, 0, 0, 2400, false, 7,
                "East-facing plot with wide internal roads, underground drainage and a community park.\n" +
                "Clear title documents available for inspection."),

            P("Independent House with Terrace", "Pune", "Kothrud",
                ListingType.Sale, PropertyType.House, 21_500_000, 3, 3, 2100, true, 8,
                "Two-floor house with an open terrace, a small front garden and a covered car porch.\n" +
                "Quiet lane close to markets and schools."),

            P("2 BHK Ready-to-Move Flat", "Chennai", "Velachery",
                ListingType.Sale, PropertyType.Apartment, 7_800_000, 2, 2, 980, false, 9,
                "Efficient layout with a separate utility area, covered parking and lift access.\n" +
                "Near the railway station and a large mall."),

            P("Retail Shop on a Main Road", "Bengaluru", "Jayanagar 4th Block",
                ListingType.Sale, PropertyType.Commercial, 28_000_000, 0, 1, 850, false, 10,
                "Ground-floor shop with a wide frontage on a busy shopping street and a mezzanine for storage."),

            P("3 BHK Builder Floor for Rent", "Gurugram", "Sector 56",
                ListingType.Rent, PropertyType.House, 55_000, 3, 3, 1800, false, 12,
                "Spacious builder floor with a dedicated parking space, lift and power backup.\n" +
                "Close to the metro station and a hospital.")
        };
    }
}
