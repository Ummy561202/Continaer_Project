using Continaer_Project.Models;
using Continaer_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Continaer_Project.Pages;

/// <summary>Development-only inbox so you can see submitted inquiries. Returns 404 elsewhere.</summary>
public class InquiriesModel : PageModel
{
    private readonly IPropertyService _properties;
    private readonly IWebHostEnvironment _env;

    public InquiriesModel(IPropertyService properties, IWebHostEnvironment env)
    {
        _properties = properties;
        _env = env;
    }

    public IReadOnlyList<Inquiry> Inquiries { get; private set; } = Array.Empty<Inquiry>();

    public IActionResult OnGet()
    {
        if (!_env.IsDevelopment()) return NotFound();

        Inquiries = _properties.GetInquiries();
        return Page();
    }
}
