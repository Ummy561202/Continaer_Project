using System.ComponentModel.DataAnnotations;

namespace Continaer_Project.Models;

/// <summary>What the "Contact the agent" form on a property page posts.</summary>
public class InquiryInput
{
    [Required, StringLength(100)]
    [Display(Name = "Your name")]
    public string Name { get; set; } = "";

    [Required, EmailAddress, StringLength(200)]
    public string Email { get; set; } = "";

    [Phone, StringLength(30)]
    [Display(Name = "Phone (optional)")]
    public string? Phone { get; set; }

    [Required, StringLength(2000, MinimumLength = 10)]
    public string Message { get; set; } = "";
}
