using Continaer_Project.Models;
using Continaer_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Continaer_Project.Pages.Listings;

public class DetailsModel : PageModel
{
    private readonly IPropertyService _properties;

    public DetailsModel(IPropertyService properties)
    {
        _properties = properties;
    }

    public Property Property { get; private set; } = null!;
    public IReadOnlyList<Property> Similar { get; private set; } = Array.Empty<Property>();

    [BindProperty]
    public InquiryInput Input { get; set; } = new();

    public IActionResult OnGet(int id)
    {
        return Load(id) ? Page() : NotFound();
    }

    public IActionResult OnPost(int id)
    {
        if (!Load(id)) return NotFound();

        if (!ModelState.IsValid) return Page();

        var inquiry = _properties.AddInquiry(id, Input);
        if (inquiry is null) return NotFound();

        TempData["StatusMessage"] = "Thanks! Your message has been sent to the owner.";
        return RedirectToPage(new { id });
    }

    private bool Load(int id)
    {
        var property = _properties.GetById(id);
        if (property is null) return false;

        Property = property;
        Similar = _properties.GetSimilar(property, 3);
        return true;
    }
}
