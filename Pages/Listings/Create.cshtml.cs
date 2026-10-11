using Continaer_Project.Models;
using Continaer_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Continaer_Project.Pages.Listings;

public class CreateModel : PageModel
{
    private readonly IPropertyService _properties;

    public CreateModel(IPropertyService properties)
    {
        _properties = properties;
    }

    [BindProperty]
    public ListingInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var created = _properties.Add(Input);
        TempData["StatusMessage"] = "Your property has been listed.";
        return RedirectToPage("Details", new { id = created.Id });
    }
}
